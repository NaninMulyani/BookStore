namespace bookApi.Core.Models;

public record ProcessResponse
{
    public string? Message { get; set; }
    public bool? Result { get; set; }
}