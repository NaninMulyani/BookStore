using bookApi.Core;
using bookApi.Core.Abstractions;
using bookApi.Infrastructure.Services;
using bookApi.Persistence.Postgres;
using bookApi.Shared.Abstractions.Encryption;
using bookApi.Shared.Infrastructure;
using bookApi.Shared.Infrastructure.Api;
using bookApi.Shared.Infrastructure.Clock;
using bookApi.Shared.Infrastructure.Contexts;
using bookApi.Shared.Infrastructure.Encryption;
using bookApi.Shared.Infrastructure.Files.FileSystems;
using bookApi.Shared.Infrastructure.Initializer;
using bookApi.Shared.Infrastructure.Localization;
using bookApi.Shared.Infrastructure.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("bookApi.UnitTests")]

namespace bookApi.Infrastructure;

public static class ServiceCollection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCore();
        services.AddSharedInfrastructure();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IFileRepositoryService, FileRepositoryService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<ICartOrderService, CartOrderService>();
        services.AddScoped<IInventoryService, InventoryService>();
        services.AddSingleton<ISalter, Salter>();

        //use one of these
        //services.AddSqlServerDbContext(configuration, "sqlserver");
        services.AddPostgresDbContext(configuration, "postgres");

        services.AddFileSystemService();
        services.AddJsonSerialization();
        services.AddClock();
        services.AddContext();
        services.AddEncryption();
        services.AddCors();
        services.AddCorsPolicy();
        services.AddLocalizerJson();

        //if use azure blob service
        //make sure app setting "azureBlobService":"" is filled
        //services.AddSingleton<IAzureBlobService, AzureBlobService>();

        services.AddInitializer<CoreInitializer>();
        services.AddApplicationInitializer();
    }
}