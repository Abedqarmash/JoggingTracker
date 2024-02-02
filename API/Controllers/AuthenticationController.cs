using BusinessLogic.Users;
using Contracts.V1.User.Models;
using Contracts.V1.User.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("v1/api/authentication")]
    public class AuthenticationController : BaseController
    {
        public readonly IUsersManager _usersManager;
        public AuthenticationController(IUsersManager usersManager)
        {
            _usersManager = usersManager;
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <remarks>Login</remarks>
        /// <param name="model"></param>
        /// <response code="201">item is created.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = "Login_v1")]
        [ProducesResponseType(typeof(AuthenticationResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var resource = await _usersManager.LogIn(model);
            return Ok(resource);
        }
    }
}
