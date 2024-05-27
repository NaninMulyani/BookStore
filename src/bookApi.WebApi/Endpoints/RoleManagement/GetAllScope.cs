using bookApi.WebApi.Common;
using bookApi.WebApi.Contracts.Responses;
using bookApi.WebApi.Endpoints.RoleManagement.Scopes;
using bookApi.WebApi.Scopes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace bookApi.WebApi.Endpoints.RoleManagement;

public class GetAllScope : BaseEndpoint<List<ScopeResponse>>
{
    [HttpGet("roles/scopes")]
    [Authorize]
    [RequiredScope(typeof(RoleManagementScope), typeof(RoleManagementScopeReadOnly))]
    [SwaggerOperation(
        Summary = "Get all scopes API",
        Description = "",
        OperationId = "RoleManagement.GetAllScope",
        Tags = new[] { "RoleManagement" })
    ]
    [ProducesResponseType(typeof(List<ScopeResponse>), StatusCodes.Status200OK)]
    public override Task<ActionResult<List<ScopeResponse>>> HandleAsync(
        CancellationToken cancellationToken = new())
    {
        var list = new List<ScopeResponse>();

        foreach (var item in ScopeManager.Instance.GetAllScopes())
            list.Add(new ScopeResponse
            {
                Name = item
            });

        return Task.FromResult<ActionResult<List<ScopeResponse>>>(list);
    }
}