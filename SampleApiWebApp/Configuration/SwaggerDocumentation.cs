using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace SampleApiWebApp.Configuration
{
    public static class SwaggerDocumentation
    {
        public static void ConfigureSwagger(this IServiceCollection services, string versionName, string title)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(versionName, new Info { Title = title, Version = versionName });
            });
        }
    }
}
