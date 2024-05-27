using bookApi.WebApi.Scopes;

namespace bookApi.WebApi.Endpoints.BookManagement.Scopes;

public class BookManagementScope : IScope
{
    public string ScopeName => nameof(BookManagementScope).ToLower();
}