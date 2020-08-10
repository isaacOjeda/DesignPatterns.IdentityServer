using System.Linq;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesignPatterns.IdentityServer.Controllers
{
    /// <summary>
    /// Api protegida por este mismo IDentity Server
    /// </summary>
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    [Route("api/Users")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult MeTest()
        {
            return Ok(new
            {
                User.Identity.Name,
                Claims = User.Claims.Select(s => new { s.Type, s.Value }).ToList()
            });
        }
    }
}