﻿using bookApi.Shared.Abstractions.Cache;
using bookApi.WebApi.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace bookApi.WebApi.Endpoints.Sample;

public class SetCache : BaseEndpointWithoutResponse<SetCacheRequest>
{
    private readonly ICache _cache;

    public SetCache(ICache cache)
    {
        _cache = cache;
    }

    [HttpPost("cache")]
    [SwaggerOperation(
        Summary = "Get data from cache by key",
        Description = "",
        OperationId = "Sample.SetCache",
        Tags = new[] { "Sample" })
    ]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public override async Task<ActionResult> HandleAsync([FromBody] SetCacheRequest request,
        CancellationToken cancellationToken = new())
    {
        await _cache.SetAsync(request.Key, request.Value);

        return Ok();
    }
}