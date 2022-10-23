using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Portfol.io.Chat.API.Controllers
{
    [Authorize]
    [ApiController]
    public class BaseController : Controller
    {
        internal Guid UserId => HttpContext.User.Identity!.IsAuthenticated ? Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value) : Guid.Empty;
    }
}