using bookApi.WebApi.Scopes;

namespace bookApi.WebApi.Endpoints.RoleManagement.Scopes;

public class RoleManagementScopeReadOnly : IScope
{
    public string ScopeName => $"{nameof(RoleManagementScope)}.readonly".ToLower();
}