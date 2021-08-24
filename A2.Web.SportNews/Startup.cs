using A2.Web.SportNews.Database;
using A2.Web.SportNews.Modules;
using A2.Web.SportNews.Options;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace A2.Web.SportNews
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("InMemoryDb"));

            services.AddControllers();
            var corsOptions = new CorsPolicyOptions();
            Configuration.GetSection(CorsPolicyOptions.SectionName).Bind(corsOptions);
            services.AddCors(options =>
                options.AddPolicy(name: corsOptions.Key, builder =>
                {
                    if (corsOptions.AllowedHosts == null || corsOptions.AllowedHosts.Length == 0)
                        builder.AllowAnyOrigin();
                    else 
                        builder.WithOrigins(corsOptions.AllowedHosts);
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                }));

            var authOptions = new AuthOptions();
            Configuration.GetSection(AuthOptions.SectionName).Bind(authOptions);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = authOptions.Issuer,

                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = authOptions.Audience,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,

                        // установка ключа безопасности
                        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<GeneralModule>();
            builder.RegisterModule<OptionsModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiContext context)
        {
            if (context.Database.IsInMemory())
                context.Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // TODO enable after SSL cert is ready
            //app.UseHttpsRedirection();

            var corsPolicy = new CorsPolicyOptions();
            Configuration.GetSection(CorsPolicyOptions.SectionName).Bind(corsPolicy);
            app.UseCors(corsPolicy.Key);
            
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
