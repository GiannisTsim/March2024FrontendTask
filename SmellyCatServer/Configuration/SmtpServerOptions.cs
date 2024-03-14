using System.ComponentModel.DataAnnotations;

namespace SmellyCatServer.Configuration;

public record SmtpServerOptions
{ 
    [Required] public string Host { get; set; } = default!;
    [Required] public int Port { get; set; } = default!;
    [Required] public bool UseSsl { get; set; } = default!;
};