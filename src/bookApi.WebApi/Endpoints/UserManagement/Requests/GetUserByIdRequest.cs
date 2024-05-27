using Microsoft.AspNetCore.Mvc;

namespace bookApi.WebApi.Endpoints.UserManagement.Requests;

public class GetUserByIdRequest
{
    [FromRoute(Name = "userId")] public Guid UserId { get; set; }
}