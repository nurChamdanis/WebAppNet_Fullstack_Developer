using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebAppNet.Middleware
{
    public class SessionCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Allow access to the login page
            if (context.Session.GetString("UserSession") == null)
            {
                if (!context.Request.Path.StartsWithSegments("/Account/Login"))
                {
                    context.Response.Redirect("/Account/Login");
                    return;
                }
            }

            await _next(context);
        }


    }
}
