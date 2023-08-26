using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using WatchCatalog_MVC.ViewModels;

namespace WatchCatalog_MVC.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var message = context.Exception.Message;
            var statusCode = StatusCodes.Status500InternalServerError;

            if (context.Exception is HttpRequestException)
            {
                statusCode = (int?)((HttpRequestException)context.Exception).StatusCode ?? (int)StatusCodes.Status500InternalServerError;
            }

            if (!context.ExceptionHandled)
            {
                if (context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
                {
                    context.Result = statusCode switch
                    {
                        404 => new NotFoundObjectResult(message),
                        400 => new BadRequestObjectResult(message),
                        _ => new StatusCodeResult(statusCode),
                    };
                }
                    
                else
                    context.Result = new RedirectToActionResult("Index", "Error", new ErrorViewModel { StatusCode =  statusCode, Message = message });
            }

            context.ExceptionHandled = true;
        }
    }
}
