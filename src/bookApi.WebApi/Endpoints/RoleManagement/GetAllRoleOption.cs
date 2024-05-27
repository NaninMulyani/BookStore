using bookApi.Shared.Abstractions.Queries;
using bookApi.WebApi.Common;
using bookApi.WebApi.Contracts.Responses;
using bookApi.WebApi.Endpoints.RoleManagement.Scopes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace bookApi.WebApi.Endpoints.RoleManagement;

public class GetAllRoleOption : BaseEndpoint<QueryOption>
{
    [HttpOptions("roles")]
    [Authorize]
    [RequiredScope(typeof(RoleManagementScope), typeof(RoleManagementScopeReadOnly))]
    [SwaggerOperation(
        Summary = "Get user paginated options",
        Description = "",
        OperationId = "RoleManagement.GetAllRoleOption",
        Tags = new[] { "RoleManagement" })
    ]
    [ProducesResponseType(typeof(QueryOption), StatusCodes.Status200OK)]
    public override Task<ActionResult<QueryOption>> HandleAsync(
        CancellationToken cancellationToken = new())
    {
        var options = new QueryOption();

        //setup columns
        options.Columns.Add(new Column(nameof(RoleResponse.Name)) { EnableOrder = true });
        options.Columns.Add(new Column(nameof(RoleResponse.Description)));

        options.EnableGlobalSearch = true;

        return Task.FromResult<ActionResult<QueryOption>>(options);
    }
}