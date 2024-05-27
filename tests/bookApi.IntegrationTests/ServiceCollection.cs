using bookApi.Core.Abstractions;
using bookApi.Infrastructure.Services;
using bookApi.IntegrationTests.Helpers;
using bookApi.Shared.Abstractions.Clock;
using bookApi.Shared.Abstractions.Contexts;
using bookApi.Shared.Abstractions.Encryption;
using bookApi.Shared.Abstractions.Files;
using bookApi.Shared.Infrastructure.Cache;
using bookApi.Shared.Infrastructure.Clock;
using bookApi.Shared.Infrastructure.Encryption;
using bookApi.Shared.Infrastructure.Localization;
using bookApi.Shared.Infrastructure.Serialization;
using bookApi.WebApi.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AuthManager = bookApi.IntegrationTests.Helpers.AuthManager;

namespace bookApi.IntegrationTests;

public static class ServiceCollection
{
    public static void AddDefaultInjectedServices(this IServiceCollection services)
    {
        services.AddScoped<IContext>(_ => new Context(Guid.Empty));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAuthManager, AuthManager>();
        services.AddSingleton<IFileService, FileSystemServiceMock>();
        services.AddInternalMemoryCache();
        services.AddJsonSerialization();
        services.AddSingleton<IClock, Clock>();
        services.AddSingleton<ISalter, Salter>();
        services.AddEncryption();
        services.AddDistributedMemoryCache();
        services.AddLocalizerJson();
        services.AddSingleton(new ClockOptions());
        services.AddScoped<IFileRepositoryService, FileRepositoryService>();
    }

    public static void EnsureDbCreated<T>(this IServiceCollection services) where T : DbContext
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var context = scopedServices.GetRequiredService<T>();
        context.Database.EnsureCreated();
    }
}