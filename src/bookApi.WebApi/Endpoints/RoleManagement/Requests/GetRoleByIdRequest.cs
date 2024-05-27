using Microsoft.AspNetCore.Mvc;

namespace bookApi.WebApi.Endpoints.RoleManagement.Requests;

public class GetRoleByIdRequest
{
    [FromRoute] public Guid RoleId { get; set; }
}