using bookApi.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace bookApi.WebApi.Endpoints.RoleManagement.Requests;

public class GetAllRolePaginatedRequest : BasePaginationCalculation
{
    [FromQuery(Name = "s")] public string? Search { get; set; }
}