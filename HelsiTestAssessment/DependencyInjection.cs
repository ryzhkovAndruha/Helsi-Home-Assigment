using HelsiTestAssesment.Infrastucture.Extensions;
using HelsiTestAssesment.Application.Extensions;
using HelsiTestAssessment.Extensions;

namespace HelsiTestAssessment;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAutoMapper(typeof(Program).Assembly)
            .AddValidation()
            .AddMongoDbServices(configuration)
            .AddApplicationServices()
            .AddInfrastructureServices();

        return services;
    }
}
