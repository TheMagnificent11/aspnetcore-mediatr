using Microsoft.Extensions.DependencyInjection;

namespace SampleApiWebApp.Configuration
{
    public static class CorsPolicy
    {
        public static void ConfigureCors(this IServiceCollection services, string policyName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    policyName,
                    policy =>
                        policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }
    }
}
