﻿using bookApi.Shared.Abstractions.Databases;
using bookApi.Shared.Infrastructure.Initializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace bookApi.Persistence.Postgres;

public static class ServiceCollection
{
    public const string DefaultConfigName = "DefaultConnection";

    public static void AddPostgresDbContext(this IServiceCollection services, IConfiguration configuration,
        string connStringName = DefaultConfigName)
    {
        services.AddDbContext<PostgresDbContext>(
            x => x.UseNpgsql(configuration.GetConnectionString(connStringName))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<PostgresDbContext>());

        services.AddInitializer<AutoMigrationService>();
    }

    public static void AddPostgresDbContext(this IServiceCollection services, IConfiguration configuration,
        Action<NpgsqlDbContextOptionsBuilder>? action,
        string connStringName = DefaultConfigName)
    {
        services.AddDbContext<PostgresDbContext>(x =>
            x.UseNpgsql(configuration.GetConnectionString(connStringName), action)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<PostgresDbContext>());

        services.AddInitializer<AutoMigrationService>();
    }
}