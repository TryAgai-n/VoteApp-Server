using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VoteApp.Host.ExceptionFilter;

public class CustomExceptionAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            UnauthorizedAccessException => new UnauthorizedResult(),
            InvalidOperationException   => new BadRequestObjectResult("Error\n" + context.Exception.Message),
            ArgumentException           => new BadRequestObjectResult("Error\n" + context.Exception.Message),
            _ => context.Result
        };
    }
}