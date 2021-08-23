using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace A2.Web.SportNews.Options
{
    public static class OptionsExtensions
    {
        public static void RegisterOptions<TOptions>(this ContainerBuilder builder, string sectionName)
            where TOptions : class, IOptions<TOptions>, new()
        {
            builder.Register(context =>
                {
                    TOptions options = new TOptions();
                    var configuration = context.Resolve<IConfiguration>();
                    configuration.GetSection(sectionName).Bind(options);
                    return options;
                })
                .SingleInstance()
                .AsSelf()
                .AutoActivate();
        }
    }
}
