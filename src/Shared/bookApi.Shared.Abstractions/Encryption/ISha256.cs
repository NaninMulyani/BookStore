﻿namespace bookApi.Shared.Abstractions.Encryption;

public interface ISha256
{
    string Hash(string data);
}