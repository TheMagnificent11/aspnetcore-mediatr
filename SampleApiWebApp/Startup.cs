using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Features.Variance;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RequestManagement;
using SampleApiWebApp.Configuration;

namespace SampleApiWebApp
{
    public sealed class Startup
    {
        private const string ApiName = "Sample API";
        private const string CorsPlolicyName = "CorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; set; }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (env == null) throw new ArgumentNullException(nameof(env));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(CorsPlolicyName);
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ApiName);
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            Migrate(app);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContextPool<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.ConfigureCors(CorsPlolicyName);
            services.AddAutoMapper();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.ConfigureProblemDetails();
            services.ConfigureSwagger("v1", ApiName);

            var builder = new ContainerBuilder();
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.Populate(services);

            builder.RegisterModule(new DataModule());
            builder.RegisterModule(new RequestManagementModule(new Assembly[] { typeof(Startup).Assembly }));

            return new AutofacServiceProvider(builder.Build());
        }

        private static void Migrate(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
                context.Database.Migrate();
            }
        }
    }
}
