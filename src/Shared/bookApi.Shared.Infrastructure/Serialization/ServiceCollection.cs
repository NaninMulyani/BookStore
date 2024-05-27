using bookApi.Shared.Abstractions.Serialization;
using bookApi.Shared.Infrastructure.Serialization.SystemTextJson;
using Microsoft.Extensions.DependencyInjection;

namespace bookApi.Shared.Infrastructure.Serialization;

public static class ServiceCollection
{
    public static void AddJsonSerialization(this IServiceCollection services)
    {
        services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();
    }
}