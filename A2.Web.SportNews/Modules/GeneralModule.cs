using A2.Web.SportNews.Abstract;
using A2.Web.SportNews.Api.Abstract;
using A2.Web.SportNews.Api.Services;
using A2.Web.SportNews.Auth;
using A2.Web.SportNews.Auth.Abstract;
using A2.Web.SportNews.Auth.Services;
using A2.Web.SportNews.Database;
using Autofac;

namespace A2.Web.SportNews.Modules
{
    public class GeneralModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContactPersonsService>().As<IContactPersonsService>();
            builder.RegisterType<NewsService>().As<INewsService>();
            builder.RegisterType<UserRightsService>().As<IUserRightsService>();
            builder.RegisterType<IdentityService>().As<IIdentityService>().AutoActivate();
            builder.RegisterType<PasswordService>().As<IPasswordService>();
            builder.RegisterType<PasswordHasher>().As<IPasswordHasher>();
            builder.RegisterType<JwtTokenBuilder>().AsSelf();
            builder.RegisterType<UnitOfWorkFactory>().AsSelf();
            builder.RegisterType<FileUploadService>().AsSelf();
        }
    }
}
