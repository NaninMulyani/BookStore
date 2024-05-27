using bookApi.WebApi.Scopes;

namespace bookApi.WebApi.Endpoints.CartManagement.Scopes;

public class CartScope : IScope
{
    public string ScopeName => nameof(CartScope).ToLower();
}