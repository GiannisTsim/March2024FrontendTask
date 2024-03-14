using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SmellyCatServer.Configuration;
using SmellyCatServer.Core;
using SmellyCatServer.Models;

namespace SmellyCatServer.Infrastructure;

public class SmtpService : IContactEmailSender
{
    private readonly ContactEmailOptions _contactEmailOptions;
    private readonly SmtpServerOptions _smtpServerOptions;

    public SmtpService(IOptions<ContactEmailOptions> contactEmailOptions, IOptions<SmtpServerOptions> smtpServerOptions )
    {
        _contactEmailOptions = contactEmailOptions.Value;
        _smtpServerOptions = smtpServerOptions.Value;
    }

    public async Task SendContactEmailAsync(ContactRequest contactRequest)
    {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_contactEmailOptions.DisplayNameFrom, _contactEmailOptions.EmailFrom));
            message.To.Add(new MailboxAddress(_contactEmailOptions.DisplayNameTo, _contactEmailOptions.EmailTo));
            message.ReplyTo.Add(new MailboxAddress(contactRequest.FullName, contactRequest.Email));
            message.Subject = $"{contactRequest.FullName} ({contactRequest.Address},{contactRequest.City} {contactRequest.PostalCode})";
            message.Body = new TextPart("plain") { Text = contactRequest.Message ?? "" };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_smtpServerOptions.Host, _smtpServerOptions.Port, _smtpServerOptions.UseSsl);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
    }
}