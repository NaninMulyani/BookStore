﻿using bookApi.Shared.Abstractions.Clock;

namespace bookApi.Shared.Infrastructure.Clock;

public sealed class Clock : IClock
{
    private readonly ClockOptions _options;

    public Clock(ClockOptions options)
    {
        _options = options;
    }

    public DateTime CurrentDate() => DateTime.UtcNow;

    public DateTime CurrentServerDate() => CurrentDate().AddHours(_options.Hours);
}