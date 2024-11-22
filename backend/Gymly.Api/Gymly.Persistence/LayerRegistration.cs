using Gymly.Business.Abstractions;
using Gymly.Persistence.Providers;
using Gymly.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gymly.Persistence;

public static class LayerRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISportClassRepository, SportClassRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IIdentityRepository, IdentityRepository>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddScoped<IDbProvider, DbProvider>(p => new DbProvider(connectionString));

        return services;
    }
}
