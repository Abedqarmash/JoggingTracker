using BusinessLogic.JoggingTracker;
using Contracts.V1.Jogging.Filters;
using Contracts.V1.Jogging.Models;
using Contracts.V1.Jogging.Resources;
using Contracts.V1.SharedModels;
using DataAccess.SQL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, User")]
    [Route("v1/api/records")]
    public class JoggingTrackerController : BaseController
    {
        private readonly IJoggingTrackerManager _manager;

        public JoggingTrackerController(IJoggingTrackerManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        ///  Get weekly reports for records.
        /// </summary>
        /// <remarks>Get generated report.</remarks>
        /// <response code="200">records are returned.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("reports", Name = "GetReports_v1")]
        [ProducesResponseType(typeof(IEnumerable<ReportResource>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReports() =>
            Ok(await _manager.GetReports());
        

        /// <summary>
        ///  Get list of records.
        /// </summary>
        /// <remarks>Get filtered records.</remarks>
        /// <param name="filter"></param>
        /// <response code="200">records are returned.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("", Name = "GetAllRecords_v1")]
        [ProducesResponseType(typeof(ResourceCollection<JoggingEntity>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllRecords([FromQuery] JoggingTrackerFilter? filter)
        {
            var chronometer = new Stopwatch();
            chronometer.Start();
            var resource = await _manager.GetRecoreds(filter);
            resource.ElapsedMilliseconds = chronometer.ElapsedMilliseconds;
            return Ok(resource);
        }

        /// <summary>
        ///  Get record by id.
        /// </summary>
        /// <remarks>Get record by id.</remarks>
        /// <param name="id"></param>
        /// <response code="200">record is returned.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}", Name = "GetRecordById_v1")]
        [ProducesResponseType(typeof(JoggingEntity), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRecordById([FromRoute] int id) =>
             Ok(await _manager.GetRecoredById(id));


        /// <summary>
        ///  Create a record.
        /// </summary>
        /// <remarks>Create new record.</remarks>
        /// <param name="model"></param>
        /// <response code="201">record is created.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = "CreateRecord_v1")]
        [ProducesResponseType(typeof(JoggingEntity), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateRecord([FromBody] JoggingModel model) =>
             Ok(await _manager.CreateRecord(model));

        /// <summary>
        ///  Update a record.
        /// </summary>
        /// <remarks>Update record.</remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">record is updated.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}", Name = "UpdateRecord_v1")]
        [ProducesResponseType(typeof(JoggingEntity), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateRecord([FromRoute] int id, [FromBody] JoggingModel model) =>
             Ok(await _manager.UpdateRecord(id, model));

        /// <summary>
        ///  delete a record.
        /// </summary>
        /// <remarks>delete record.</remarks>
        /// <param name="id"></param>
        /// <response code="200">record is deleted.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}", Name = "DeleteRecord_v1")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteeRecord([FromRoute] int id)
        {
            await _manager.DeleteRecord(id);
            return NoContent();
        }
    }
}
