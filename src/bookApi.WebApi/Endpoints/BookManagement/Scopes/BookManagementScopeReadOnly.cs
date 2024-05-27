using bookApi.WebApi.Scopes;

namespace bookApi.WebApi.Endpoints.BookManagement.Scopes;

public class BookManagementScopeReadOnly : IScope
{
    public string ScopeName => $"{nameof(BookManagementScope)}.readonly".ToLower();
}