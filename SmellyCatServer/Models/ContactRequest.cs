using System.ComponentModel.DataAnnotations;

namespace SmellyCatServer.Models;

public record ContactRequest
{
    [Required] public string? FullName { get; init; }
    [Required, EmailAddress] public string? Email { get; init; }
    [Required] public string? City { get; init; }
    [Required] public string? PostalCode { get; init; }
    [Required] public string? Address { get; init; }
    public string? Message { get; init; }
}