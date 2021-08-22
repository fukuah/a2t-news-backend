using A2.Web.SportNews.Abstract;
using A2.Web.SportNews.Entities;
using A2.Web.SportNews.Repositories;
using A2.Web.SportNews.Services;
using Autofac;

namespace A2.Web.SportNews.Modules
{
    public class GeneralModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContactPersonsService>().As<IContactPersonsService>();
            builder.RegisterType<NewsService>().As<INewsService>();
            builder.RegisterType<ContactPersonRepository>().As<IRepository<ContactPersonEntity>>();
            builder.RegisterType<NewsRepository>().As<IRepository<NewsEntity>>();
            builder.RegisterType<FileUploadService>().AsSelf();
        }
    }
}
