using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace testApp.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GetTimeMiddleware
    {
        public readonly RequestDelegate _next;

        public GetTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }
    
        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();
            var content = context.Request.Path.ToString();
            var bodyAsText = await new System.IO.StreamReader(context.Request.Body).ReadLineAsync();
            var user = context.User.Identity.Name ?? "Guest";
            if (context.Request.Body.Length!=0)
            {
                content = Regex.Replace(bodyAsText, @"__RequestVerificationToken.*", string.Empty).Replace('&', ' ');
                context.Request.Body.Position = 0;
            }
            using var stream = new StreamWriter(@"C:\Users\vicbo\Desktop\123.txt", true);
            stream.Write($"{DateTime.Now}({user}) {context.Request.Method}: {content} \n");
            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TestMiddlewareExtensions
    {
        public static IApplicationBuilder UseTestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GetTimeMiddleware>();
        }
    }
}
