using bookApi.Shared.Abstractions.Queries;

namespace bookApi.WebApi.Endpoints.CartManagement.Requests;

public class GetAllCartRequest : BasePaginationCalculation
{
    public string? UserName { get; set; }
    public string? BookTitle { get; set; }
}