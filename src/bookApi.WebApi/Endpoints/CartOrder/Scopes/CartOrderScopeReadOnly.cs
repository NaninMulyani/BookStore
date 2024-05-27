using bookApi.WebApi.Scopes;

namespace bookApi.WebApi.Endpoints.CartOrder.Scopes;

public class CartOrderScopeReadOnly : IScope
{
    public string ScopeName => $"{nameof(CartOrderScope)}.readonly".ToLower();
}