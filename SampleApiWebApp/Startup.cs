using System;
using System.Reflection;
using Autofac;
using Autofac.Features.Variance;
using AutofacSerilogIntegration;
using AutoMapper;
using EntityManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RequestManagement;
using RequestManagement.Logging;
using SampleApiWebApp.Configuration;
using SampleApiWebApp.Data;
using Serilog.Events;

namespace SampleApiWebApp
{
    public class Startup
    {
        private const string ApiName = "Sample API";
        private const string ApiVersion = "v1";

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ConfigureSwagger(ApiVersion, ApiName);

            Migrate(app);
        }

        public static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterLogger();

            builder.RegisterSource(new ContravariantRegistrationSource());

            builder.RegisterModule(new EntityManagementModule<DatabaseContext>());
            builder.RegisterModule(new RequestManagementModule(new Assembly[] { typeof(Startup).Assembly }));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = this.GetSettings<ApplicationSettings>("ApplicationSettings");
            var seqSettings = this.GetSettings<SeqSettings>("SeqSettings");

            services.ConfigureLogging(this.Configuration, LogEventLevel.Debug, appSettings, seqSettings);

            services.AddDbContextPool<DatabaseContext>(options =>
            {
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));

                // TODO
                ////options.UseLoggerFactory()
            });

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddControllers(options => options.Filters.Add(new ExceptionFilter()));

            services.ConfigureProblemDetails();

            services.ConfigureSwagger(ApiVersion, ApiName);
        }

        private static void Migrate(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
                context.Database.Migrate();
            }
        }

        private T GetSettings<T>(string configurationSection)
            where T : class, new()
        {
            var settings = new T();

            this.Configuration.Bind(configurationSection, settings);

            return settings;
        }
    }
}
