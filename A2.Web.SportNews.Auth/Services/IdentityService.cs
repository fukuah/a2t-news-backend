using System;
using System.Threading.Tasks;
using A2.Web.SportNews.Auth.Abstract;
using A2.Web.SportNews.Auth.Core;
using A2.Web.SportNews.Auth.Core.Mappers;
using A2.Web.SportNews.Auth.Core.Requests;
using A2.Web.SportNews.Database;
using Serilog;

namespace A2.Web.SportNews.Auth.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UnitOfWorkFactory _uowFactory;
        private readonly IPasswordService _passwordService;

        public IdentityService(UnitOfWorkFactory uowFactory, IPasswordService passwordService)
        {
            _uowFactory = uowFactory ?? throw new ArgumentNullException(nameof(uowFactory));
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
        }

        public async Task<UserCore> GetIdentityAsync(LoginRequest request)
        {
            await using var uow = _uowFactory.Create();
            var user = await uow.IdentityRepository.GetByUsernameAsync(request.Username);

            if (user == null) return null;

            var result = _passwordService.CheckPassword(request.Password, user.PasswordHash);

            if (!result.IsValid)
            {
                Log.Information($"{nameof(IdentityService)}: an attempt to login as user \"{request.Username}\" with invalid password=\"{request.Password}\"");
                return null;
            }

            // Update password if new hash was generated
            if (!string.IsNullOrEmpty(result.NewPasswordHash))
            {
                Log.Information($"{nameof(IdentityService)}: password hash for user \"{user.Username}\" is need to be updated.");
                // Case is pretty rare, so it's better to create new UnitOfWork than linger in scope of using
                user.PasswordHash = _passwordService.GenerateHash(request.Password);
                await using var uow2 = _uowFactory.Create();
                await uow2.UsersRepository.UpdateAsync(user);
                Log.Information($"{nameof(IdentityService)}: user \"{user.Username}\" is updated, has new password hash.");
            }

            return user.ToCore();
        }
    }
}
