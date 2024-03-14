using System.ComponentModel.DataAnnotations;

namespace SmellyCatServer.Configuration;

public record ContactEmailOptions
{
    [Required] public string DisplayNameFrom { get; set; } = default!;
    [Required, EmailAddress] public string EmailFrom { get; set; } = default!;
    [Required] public string DisplayNameTo { get; set; } = default!;
    [Required, EmailAddress] public string EmailTo { get; set; } = default!;
}