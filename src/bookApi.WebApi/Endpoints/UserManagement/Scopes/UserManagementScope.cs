using bookApi.WebApi.Scopes;

namespace bookApi.WebApi.Endpoints.UserManagement.Scopes;

public class UserManagementScope : IScope
{
    public string ScopeName => nameof(UserManagementScope).ToLower();
}