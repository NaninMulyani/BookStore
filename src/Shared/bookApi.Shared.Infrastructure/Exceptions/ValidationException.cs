﻿using bookApi.Shared.Abstractions.Models;
using FluentValidation.Results;

namespace bookApi.Shared.Infrastructure.Exceptions;

/// <summary>
/// Represents an exception that occurs when a validation fails.
/// </summary>
public sealed class ValidationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    /// <param name="failures">The collection of validation failures.</param>
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : base("One or more validation failures has occurred.") =>
        Errors = failures
            .Distinct()
            .Select(failure => new Error("invalid_payload", failure.ErrorMessage))
            .ToArray();

    /// <summary>
    /// Gets the errors.
    /// </summary>
    public IReadOnlyCollection<Error> Errors { get; }
}