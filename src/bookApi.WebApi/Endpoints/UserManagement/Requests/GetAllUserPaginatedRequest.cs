using bookApi.Shared.Abstractions.Queries;

namespace bookApi.WebApi.Endpoints.UserManagement.Requests;

public class GetAllUserPaginatedRequest : BasePaginationCalculation
{
    public string? Username { get; set; }
    public string? Fullname { get; set; }
}