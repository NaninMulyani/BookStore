﻿namespace bookApi.Shared.Abstractions.Encryption;

public interface IMd5
{
    string Hash(string value);
}