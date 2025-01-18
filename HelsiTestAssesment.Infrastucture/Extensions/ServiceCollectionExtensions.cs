using HelsiTestAssesment.Application.Core.Interfaces;
using HelsiTestAssesment.Infrastucture.CQRS;
using HelsiTestAssesment.Infrastucture.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Workleap.Extensions.Mongo;
using Microsoft.Extensions.Configuration;
using HelsiTestAssesment.Infrastucture.Options;
using MongoDB.Driver;

namespace HelsiTestAssesment.Infrastucture.Extensions;

public static class ServiceCollectionExtensions
{
    private const int MongoConnectionTimeoutMinutes = 5;
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<MongoDbContext>();

        services.AddScoped<CommandDispatcher>();
        services.AddScoped<QueryDispatcher>();

        services.AddScoped<ITaskListRepository, TaskListRepository>();

        return services;
    }

    public static IServiceCollection AddMongoDbServices(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoOptions = configuration
                .GetRequiredSection(MongoOptions.SectionName)
                .Get<MongoOptions>() ?? new MongoOptions();

        var mongoUrl = MongoUrl.Create(mongoOptions.ConnectionString);
        var mongoClient = new MongoClient(mongoUrl);
        var database = mongoClient.GetDatabase(mongoOptions.DefaultDatabaseName);

        services.AddSingleton<IMongoDatabase>(database);

        return services;
    }
}
