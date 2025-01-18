using HelsiTestAssesment.Infrastucture.Extensions;
using HelsiTestAssesment.Application.Extensions; 

namespace HelsiTestAssessment;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAutoMapper(typeof(Program).Assembly)
            .AddMongoDbServices(configuration)
            .AddApplicationServices()
            .AddInfrastructureServices();

        return services;
    }
}
