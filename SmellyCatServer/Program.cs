using Microsoft.AspNetCore.Mvc;
using SmellyCatServer.Configuration;
using SmellyCatServer.Core;
using SmellyCatServer.Filters;
using SmellyCatServer.Infrastructure;
using SmellyCatServer.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<ContactEmailOptions>()
       .Bind(builder.Configuration.GetSection(nameof(ContactEmailOptions)))
       .ValidateDataAnnotations()
       .ValidateOnStart();
builder.Services.AddOptions<SmtpServerOptions>()
       .Bind(builder.Configuration.GetSection(nameof(SmtpServerOptions)))
       .ValidateDataAnnotations()
       .ValidateOnStart();

builder.Services.AddCors
(
    options =>
    {
        options.AddDefaultPolicy
        (
            policyBuilder => { policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); }
        );
    }
);

builder.Services.AddTransient<IContactEmailSender, SmtpService>();

var app = builder.Build();

app.UseCors();

app.MapPost
   (
       "/contact",
       async ([FromBody] ContactRequest contactRequest, [FromServices] IContactEmailSender emailSender) =>
       {
           await emailSender.SendContactEmailAsync(contactRequest);
           return Results.Ok(contactRequest.ToString());
       }
   )
   .AddEndpointFilter<ContactRequestValidationFilter>();

app.Run();