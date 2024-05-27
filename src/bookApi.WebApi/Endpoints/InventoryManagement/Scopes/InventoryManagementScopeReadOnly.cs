using bookApi.WebApi.Scopes;

namespace bookApi.WebApi.Endpoints.InventoryManagement.Scopes;

public class InventoryManagementScopeReadOnly : IScope
{
    public string ScopeName => $"{nameof(InventoryManagementScope)}.readonly".ToLower();
}