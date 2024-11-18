using Gymly.Infrastructure.Abstractions;
using Gymly.Infrastructure.Authentication;
using Gymly.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gymly.Infrastructure;

public static class LayerRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwt(configuration);
        services.AddScoped<IIdentityManager, IdentityManager>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
