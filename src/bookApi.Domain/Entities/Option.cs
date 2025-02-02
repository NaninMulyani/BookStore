﻿using bookApi.Shared.Abstractions.Entities;

namespace bookApi.Domain.Entities;

public sealed class Option : BaseEntity, IEntity
{
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string? Description { get; set; }
}