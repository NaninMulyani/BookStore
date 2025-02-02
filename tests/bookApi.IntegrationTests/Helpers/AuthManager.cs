﻿using bookApi.WebApi.Common;

namespace bookApi.IntegrationTests.Helpers;

public class AuthManager : IAuthManager
{
    public AuthManager()
    {
        Options = new AuthOptions();
    }

    public AuthOptions Options { get; }

    public JsonWebToken CreateToken(string uniqueIdentifier, string? audience = null,
        IDictionary<string, IEnumerable<string>>? claims = null) => new() { AccessToken = "abcde", Expiry = 1 };
}