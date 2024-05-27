using bookApi.WebApi.Scopes;

namespace bookApi.WebApi.Endpoints.RoleManagement.Scopes;

public class RoleManagementScope : IScope
{
    public string ScopeName => nameof(RoleManagementScope).ToLower();
}