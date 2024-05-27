using bookApi.WebApi.Validators;
using FluentValidation;

namespace bookApi.WebApi.Endpoints.CartOrder.Requests;

public class CreateCartOrderRequestValidator : AbstractValidator<CreateCartOrderRequest>
{
    public CreateCartOrderRequestValidator()
    {
        RuleFor(e => e.OrderDate).NotNull().NotEmpty();
    }
}