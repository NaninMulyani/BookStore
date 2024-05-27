using FluentValidation;

namespace bookApi.WebApi.Endpoints.CartOrder.Requests;

public class UpdateCartOrderRequestPayloadValidator : AbstractValidator<UpdateCartOrderRequestPayload>
{
    public UpdateCartOrderRequestPayloadValidator()
    {
        RuleFor(e => e.OrderDate).NotNull().NotEmpty();
    }
}