using System.Security.Claims;
using System.Security.Principal;

namespace Blogging.Models
{
    public class LoggedInUser : ClaimsPrincipal, IIdentity
    {
        public string? Name { get; set; }
        public string? AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}