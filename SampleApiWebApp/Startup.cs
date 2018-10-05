using System;
using System.Net;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Features.Variance;
using AutoMapper;
using EntityManagement;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RequestManagement;
using SampleApiWebApp.Constants;
using Swashbuckle.AspNetCore.Swagger;

namespace SampleApiWebApp
{
    /// <summary>
    /// Startup
    /// </summary>
    public sealed class Startup
    {
        private const string ApiName = "Sample API";
        private const string CorsPlolicyName = "CorsPolicy";

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class
        /// </summary>
        /// <param name="configuration">Application configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; set; }

        /// <summary>
        /// Configures the HTTP request pipeline
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="env">Hosting environment</param>
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

        /// <summary>
        /// Configures application services
        /// </summary>
        /// <param name="services">Services collection</param>
        /// <returns>Service provider</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            ConfigureCors(services);

            services.AddDbContextPool<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            ConfigureProblemDetails(services);
            ConfigureSwagger(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAutoMapper();
            ////ConfigureMediatr(services);

            var builder = new ContainerBuilder();
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.Populate(services);

            RegisterDataAccessTypes(builder);
            RegisterMediatrHandlers(builder);

            return new AutofacServiceProvider(builder.Build());
        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    CorsPlolicyName,
                    policy =>
                        policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        private static void ConfigureProblemDetails(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = (int)HttpStatusCode.BadRequest,
                        Detail = "Please refer to the errors property for additional details"
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { ContentTypes.ApplicationJson }
                    };
                };
            });
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = ApiName, Version = "v1" });
            });
        }

        private static void ConfigureMediatr(IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(Startup).Assembly,
                typeof(OperationResult).Assembly
            };

            services.AddMediatR(assemblies);
        }

        private static void Migrate(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
                context.Database.Migrate();
            }
        }

        private static void RegisterDataAccessTypes(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseContext>()
                .As<IDatabaseContext>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EntityRepository<,>))
                .As(typeof(IEntityRepository<,>));
        }

        private static void RegisterMediatrHandlers(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            ////var mediatrOpenTypes = new[]
            ////{
            ////    typeof(IRequestHandler<,>),
            ////    typeof(INotificationHandler<>),
            ////};

            ////var assemblies = new[]
            ////{
            ////    typeof(Startup).Assembly,
            ////    typeof(OperationResult).Assembly
            ////};

            ////foreach (var assembly in assemblies)
            ////{
            ////    foreach (var mediatrOpenType in mediatrOpenTypes)
            ////    {
            ////        builder
            ////            .RegisterAssemblyTypes(assembly)
            ////            .AsClosedTypesOf(mediatrOpenType)
            ////            .AsImplementedInterfaces();
            ////    }
            ////}

            builder.RegisterType<PostRequestHandler<long, Domain.Team, Models.Team, Models.CreateTeamRequest>>()
                .As<IRequestHandler<Models.CreateTeamRequest, OperationResult<long>>>();

            builder.RegisterType<GetOneHandler<long, Domain.Team, Models.GetTeamRequest, Models.Team>>()
                .As<IRequestHandler<Models.GetTeamRequest, OperationResult<Models.Team>>>();

            ////builder.RegisterGeneric(typeof(PostRequestHandler<,,,>))
            ////    .AsImplementedInterfaces();

            ////builder.RegisterGeneric(typeof(GetOneHandler<,,,>))
            ////    .AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
