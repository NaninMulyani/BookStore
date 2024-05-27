using FluentValidation;

namespace bookApi.WebApi.Endpoints.UserManagement.Requests;

public class UpdateUserRequestPayloadValidator : AbstractValidator<UpdateUserRequestPayload>
{
    public UpdateUserRequestPayloadValidator()
    {
        RuleFor(e => e.Fullname).NotNull().NotEmpty();
    }
}