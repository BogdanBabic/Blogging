using Blogging.Models;
using Newtonsoft.Json;
using System.Security.Principal;

namespace Blogging.Middleware
{
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _context;

        public Middleware(RequestDelegate next, IHttpContextAccessor context)
        {
            _context = context;
            _next = next;
        }

        public async Task Invoke(HttpContext ctx)
        {
            var cookie = ctx.Request.Cookies["User"];

            if (!string.IsNullOrEmpty(cookie))
            {
                var loggedInUser = JsonConvert.DeserializeObject<User>(cookie);

                var user = new LoggedInUser();
                user.Name = loggedInUser!.Username;
                user.IsAuthenticated = true;
                string[]? roles = { "" };

                var currentUser = new GenericPrincipal(user, roles);

                _context!.HttpContext!.User = currentUser;
            }

            await _next(ctx);
        }
    }
}