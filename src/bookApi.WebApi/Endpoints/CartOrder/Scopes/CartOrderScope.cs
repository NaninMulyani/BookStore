using bookApi.WebApi.Scopes;

namespace bookApi.WebApi.Endpoints.CartOrder.Scopes;

public class CartOrderScope : IScope
{
    public string ScopeName => nameof(CartOrderScope).ToLower();
}