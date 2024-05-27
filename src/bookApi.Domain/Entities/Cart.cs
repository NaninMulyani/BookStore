﻿using bookApi.Domain.Enums;
using bookApi.Shared.Abstractions.Entities;

namespace bookApi.Domain.Entities;

public sealed class Cart : BaseEntity
{
    public Guid CartId { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public decimal? Price { get; set; }
    public int? Qty { get; set; }
    public decimal? TotalPrice { get; set; }
    public Book? Book { get; set; }
    public User? User { get; set; }
}