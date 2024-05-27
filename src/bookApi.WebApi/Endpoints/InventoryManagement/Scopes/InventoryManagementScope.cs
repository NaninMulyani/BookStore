using bookApi.WebApi.Scopes;

namespace bookApi.WebApi.Endpoints.InventoryManagement.Scopes;

public class InventoryManagementScope : IScope
{
    public string ScopeName => nameof(InventoryManagementScope).ToLower();
}