using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.Web.SportNews.Options;
using Autofac;

namespace A2.Web.SportNews.Modules
{
    public class OptionsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterOptions<AppOptions>(AppOptions.SectionName);
            builder.RegisterOptions<AuthOptions>(AuthOptions.SectionName);
            builder.RegisterOptions<CorsPolicyOptions>(CorsPolicyOptions.SectionName);
        }
    }
}
