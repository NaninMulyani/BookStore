﻿namespace bookApi.Shared.Abstractions.Clock;

public interface IClock
{
    DateTime CurrentDate();

    DateTime CurrentServerDate();
}