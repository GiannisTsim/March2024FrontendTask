using System.ComponentModel.DataAnnotations;
using SmellyCatServer.Models;

namespace SmellyCatServer.Filters;

public class ContactRequestValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var contactRequest = context.GetArgument<ContactRequest>(0);
        var validationResults = new List<ValidationResult>();

        if (Validator.TryValidateObject(contactRequest, new ValidationContext(contactRequest), validationResults, true))
        {
            return await next(context);
        }

        var errors = validationResults
                        .GroupBy(vr => vr.MemberNames.FirstOrDefault() ?? "")
                        .ToDictionary
                        (
                            g => g.Key,
                            g => g.Select(vr => vr.ErrorMessage ?? "Unknown error").ToArray()
                        );
        return Results.ValidationProblem(errors);
    }
}