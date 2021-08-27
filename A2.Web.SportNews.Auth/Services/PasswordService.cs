using System;
using A2.Web.SportNews.Auth.Abstract;

namespace A2.Web.SportNews.Auth.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher _passwordHasher;

        public PasswordService(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public string GenerateHash(string password) => _passwordHasher.Hash(password);

        public (bool IsValid, string NewPasswordHash) CheckPassword(string passwordToCheck, string userPasswordHash)
        {
            //var passwordHashToCheck = _passwordHasher.Hash(passwordToCheck);
            var checkResult = _passwordHasher.Check(userPasswordHash, passwordToCheck);

            if (!checkResult.Verified) return (false, null);

            return checkResult.NeedsUpgrade ? (true, _passwordHasher.Hash(passwordToCheck)) : (true, null);
        }
    }
}