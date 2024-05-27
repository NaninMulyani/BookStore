using Microsoft.AspNetCore.Mvc;

namespace bookApi.WebApi.Endpoints.BookManagement.Requests;

public class DeleteBookRequest
{
    [FromRoute(Name = "BookId")] public Guid BookId { get; set; }
}