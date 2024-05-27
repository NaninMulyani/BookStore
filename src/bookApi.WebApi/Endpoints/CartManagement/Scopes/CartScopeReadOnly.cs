using bookApi.WebApi.Scopes;

namespace bookApi.WebApi.Endpoints.CartManagement.Scopes;

public class CartScopeReadOnly : IScope
{
    public string ScopeName => $"{nameof(CartScope)}.readonly".ToLower();
}