using SmellyCatServer.Models;

namespace SmellyCatServer.Core;

public interface IContactEmailSender
{
    Task SendContactEmailAsync(ContactRequest contactRequest);
}