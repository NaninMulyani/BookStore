using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable RouteTemplates.RouteTokenNotResolved

namespace bookApi.WebApi.Endpoints;

[Route("api/[namespace]")]
public abstract class BaseEndpointWithoutResponse<TReq> : EndpointBaseAsync.WithRequest<TReq>.WithActionResult
{
}