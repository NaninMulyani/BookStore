﻿using bookApi.Shared.Abstractions.Encryption;
using System.Security.Cryptography;
using System.Text;

namespace bookApi.Shared.Infrastructure.Encryption;

internal class Sha512 : ISha512
{
    public string Hash(string data)
    {
        var bytes = SHA512.HashData(Encoding.UTF8.GetBytes(data));
        var builder = new StringBuilder();
        foreach (var @byte in bytes)
        {
            builder.Append(@byte.ToString("x2"));
        }

        return builder.ToString();
    }
}