using BusinessLogic.Users;
using Contracts.V1.SharedModels;
using Contracts.V1.User.Filters;
using Contracts.V1.User.Models;
using Contracts.V1.User.Resources;
using DataAccess.SQL.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, UserManager")]
    [Route("v1/api/users")]
    public class UserController : BaseController
    {
        public readonly IUsersManager _usersManager;

        public UserController(IUsersManager usersManager)
        {
            _usersManager = usersManager;
        }

        /// <summary>
        ///  Get list of users.
        /// </summary>
        /// <remarks>Get filtered list of users.</remarks>
        /// <param name="filter"></param>
        /// <response code="200">users are returned.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("", Name = "GetAllUsers_v1")]
        [ProducesResponseType(typeof(ResourceCollection<UserResource>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserListFilter? filter)
        {
            var chronometer = new Stopwatch();
            chronometer.Start();
            var resource = await _usersManager.GetAllUsers(filter);
            resource.ElapsedMilliseconds = chronometer.ElapsedMilliseconds;
            return Ok(resource);
        }

        /// <summary>
        ///  Get user by id.
        /// </summary>
        /// <remarks>Get user by id.</remarks>
        /// <param name="id"></param>
        /// <response code="200">users are returned.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Item not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}", Name = "GetUserById_v1")]
        [ProducesResponseType(typeof(UserResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsersById([FromRoute] string id) =>
             Ok(await _usersManager.GetUserById(id));

        /// <summary>
        ///  Get user by email.
        /// </summary>
        /// <remarks>Get user by email.</remarks>
        /// <param name="email"></param>
        /// <response code="200">users are returned.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Item not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{email}", Name = "GetUserByEmail_v1")]
        [ProducesResponseType(typeof(UserResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserByEmail([FromRoute] string email) =>
             Ok(await _usersManager.GetUserByEmail(email));


        /// <summary>
        ///  Register User.
        /// </summary>
        /// <remarks>Register new user.</remarks>
        /// <param name="model"></param>
        /// <response code="201">user is created.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = "CreateUser_v1")]
        [ProducesResponseType(typeof(UserResource), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateUser([FromBody] RegisterModel model)
        {
            return Ok(await _usersManager.RegisterUser(model));
        }

        /// <summary>
        ///  Update User.
        /// </summary>
        /// <remarks>Update user.</remarks>
        /// <param name="email"></param>
        /// <param name="model"></param>
        /// <response code="200">user is created.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Item not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPut]
        [Route("{email}", Name = "UpdateUser_v1")]
        [ProducesResponseType(typeof(UserResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUser([FromRoute] string email,[FromBody] RegisterModel model)
        {
            return Ok(await _usersManager.UpdateUser(email, model));
        }

        /// <summary>
        ///  Grant User Permissions.
        /// </summary>
        /// <remarks>Grant user permissions.</remarks>
        /// <param name="email"></param>
        /// <param name="roles"></param>
        /// <response code="200">user is created.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Item not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPut]
        [Route("grant/{email}", Name = "GrantUserPermission_v1")]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GrantUser([FromRoute] string email,
            [FromQuery] IEnumerable<RoleType> roles)
        {
            return Ok(await _usersManager.GrantPermission(email, roles));
        }

        /// <summary>
        ///  Revoke User Permissions.
        /// </summary>
        /// <remarks>Revoke user permissions.</remarks>
        /// <param name="email"></param>
        /// <param name="roles"></param>
        /// <response code="200">user is created.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Item not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPut]
        [Route("revoke/{email}", Name = "RevokeUserPermission_v1")]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RevokeUser([FromRoute] string email,
            [FromQuery] IEnumerable<RoleType> roles)
        {
            return Ok(await _usersManager.RevokePermission(email, roles));
        }

        /// <summary>
        ///  Delete User.
        /// </summary>
        /// <remarks>Delete user by email.</remarks>
        /// <param name="email"></param>
        /// <response code="204">user is deleted.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Item not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("{email}", Name = "DeleteUserPermission_v1")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DelteUser([FromRoute] string email)
        {
            await _usersManager.DeleteUser(email);
            return NoContent();
        }
    }
}
