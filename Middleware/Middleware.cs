using System.Net;

namespace WebApiApp.Middleware
{
    public class Middleware :IMiddleware
    {
        private bool IsAllowedIP(string ipAddress)
        {
            var allowedIPs = new List<string> { "127.0.0.1", "::1" };
                return allowedIPs.Contains(ipAddress);
        }
        public void configuration(IApplicationBuilder app)
        {
            app.UseMiddleware<Middleware>();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var remoteIpAddress = context.Connection.RemoteIpAddress?.ToString();

            if (!IsAllowedIP(remoteIpAddress))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync("Access denied");
                return;
            }       

            await next(context);
        }

    }

    public static class ClassWithNoImplementationMiddlewareExtendsions
    {
        public static IApplicationBuilder UseClassWithNoImplementationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }
}
