﻿namespace bookApi.Shared.Infrastructure;

public sealed class AppOptions
{
    public string Name { get; set; } = null!;
    public string Instance { get; set; } = null!;
    public string Version { get; set; } = null!;
}