using Microsoft.AspNetCore.Mvc;

namespace bookApi.WebApi.Endpoints.RoleManagement.Requests;

public class GetAllRoleRequest
{
    [FromQuery(Name = "s")] public string? Search { get; set; }
}