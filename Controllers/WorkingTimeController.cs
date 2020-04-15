using Api_Salary_Management.Filter;
using Api_Salary_Management.Models;
using Api_Salary_Management.Respository;
using Api_Salary_Management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api_Salary_Management.Controllers
{
    public class WorkingTimeController : ApiController
    {
        IWorkingTime workingTimeManager = new WorkingTimeManager();
        [JwtAuthentication]
        [HttpGet]
        [Route("getworkingtime")]
        /// <remarks>
        /// This is Get api
        /// </remarks>
        /// <response code="200">Returns the list of working time</response>
        /// <response code="404">working time not find</response>
        [System.Web.Http.Description.ResponseType(typeof(IList<WorkingTimeModel>))]
        public HttpResponseMessage GetWorkingTime([FromUri] WorkingTimeModel workingTime)
        {
            try
            {
                IList<WorkingTime> listtime = null;
                listtime = workingTimeManager.GetWorkingTimes(workingTime);
                if (listtime == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no working time match the search value!!!");
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { WorkingTimeModel = listtime });
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing GetWorkingTime:" + e.Message);
            }
        }

        [JwtAuthentication]
        [HttpPost]
        [Route("createworkingtime")]
        /// <remarks>
        /// This is Post api
        /// </remarks>
        /// <response code="200">Create a working time</response>
        /// <response code="404">working time not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PostNewWorkingTime(WorkingTime workingTime)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");
                workingTimeManager.CreateWorkingTime(workingTime);
                return Request.CreateResponse(HttpStatusCode.OK, "Working Time created!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing PostNewWorkingTime:" + e.Message);
            }
        }
        [JwtAuthentication]
        [HttpPut]
        [Route("updateworkingtime")]
        /// <remarks>
        /// This is Put api
        /// </remarks>
        /// <response code="200">Update a working time</response>
        /// <response code="404">working time not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PutWorkingTime(WorkingTimeModel workingTime)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");
                bool check = workingTimeManager.UpdateWorkingTime(workingTime);
                 if (check) return Request.CreateResponse(HttpStatusCode.OK, "working time updated!");
                else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "working time not found!!!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Put:" + e.Message);
            }
        }
        [JwtAuthentication]
        [HttpDelete]
        [Route("deleteworkingtime")]
        /// <remarks>
        /// This is Delete api
        /// </remarks>
        /// <response code="200">Delete a working time</response>
        /// <response code="404">working time not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage Delete(String id)
        {
            try
            {
                workingTimeManager.DeleteWorkingTime(id);
                return Request.CreateResponse(HttpStatusCode.OK, "working time deleted!");
            }

            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Delete:" + e.Message);
            }

        }
    }
}
