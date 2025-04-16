using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;
using Dashboard.Models;

namespace Dashboard.Filters
{
    public class SessionIdCookieFilter : IActionFilter
    {
        private const string CookieName = "sessionId";

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;

            if (!request.Cookies.ContainsKey(CookieName))
            {
                string sessionId = Guid.NewGuid().ToString();

                // Crear la cookie
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = request.IsHttps,
                    Expires = DateTimeOffset.Now.AddDays(30)
                };
                response.Cookies.Append(CookieName, sessionId, cookieOptions);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}