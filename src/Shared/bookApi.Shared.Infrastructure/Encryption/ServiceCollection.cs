﻿using bookApi.Shared.Abstractions.Encryption;
using Microsoft.Extensions.DependencyInjection;

namespace bookApi.Shared.Infrastructure.Encryption;

public static class ServiceCollection
{
    public static void AddEncryption(this IServiceCollection services)
    {
        services
            .AddSingleton<ISha256, Sha256>()
            .AddSingleton<ISha512, Sha512>()
            .AddSingleton<IMd5, Md5>()
            .AddSingleton<IRng, Rng>()
            .AddSingleton<IAES, AES>();

    }
}