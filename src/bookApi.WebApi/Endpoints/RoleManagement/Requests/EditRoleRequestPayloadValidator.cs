﻿using bookApi.WebApi.Scopes;
using bookApi.WebApi.Validators;
using FluentValidation;

namespace bookApi.WebApi.Endpoints.RoleManagement.Requests;

public class EditRoleRequestPayloadValidator : AbstractValidator<EditRoleRequestPayload>
{
    public EditRoleRequestPayloadValidator()
    {
        RuleFor(e => e.Scopes).NotNull();

        When(e => !string.IsNullOrWhiteSpace(e.Description),
            () => { RuleFor(e => e.Description).SetValidator(new AsciiOnlyValidator()!).MaximumLength(512); });

        When(e => e.Scopes.Any(),
            () =>
            {
                RuleFor(e => e.Scopes).Must(e => e.Count == e.Distinct().Count());

                RuleForEach(e => e.Scopes)
                    .Must(e => ScopeManager.Instance.GetAllScopes().Any(f => f == e));
            });
    }
}