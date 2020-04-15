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
    [RoutePrefix("api/period")]
    
    public class PeriodController : ApiController
    {
        IPeriod periodManager = new PeriodManager();
        [JwtAuthentication]
        [HttpGet]
        [Route("getperiod")]
        /// <remarks>
        /// This is Get api
        /// </remarks>
        /// <response code="200">Returns the list of period</response>
        /// <response code="404">period not find</response>
        [System.Web.Http.Description.ResponseType(typeof(IList<PeriodModel>))]
        public HttpResponseMessage GetPeriod([FromUri] PeriodModel period)
        {
            try
            {
                IList<Period> listperiod = null;
                listperiod = periodManager.GetPeriods(period);
                if (listperiod == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no period's name match the search value!!!");
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { PeriodModel = listperiod });
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing GetPeriod:" + e.Message);
            }
        }

        [JwtAuthentication]
        [HttpPost]
        [Route("createperiod")]
        /// <remarks>
        /// This is Post api
        /// </remarks>
        /// <response code="200">Create a period</response>
        /// <response code="404">period not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PostNewPeriod(Period period)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");
                periodManager.CreatePeriod(period);
                return Request.CreateResponse(HttpStatusCode.OK, "Period created!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing PostNewPeriod:" + e.Message);
            }
        }
        [JwtAuthentication]
        [HttpPut]
        [Route("updateperiod")]
        /// <remarks>
        /// This is Put api
        /// </remarks>
        /// <response code="200">Update a period</response>
        /// <response code="404">period not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PutPeriod(PeriodModel period)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");

                bool check = periodManager.UpdatePeriod(period);
                if (check) return Request.CreateResponse(HttpStatusCode.OK, "Period updated!");
                else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Period not found!!!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Put:" + e.Message);
            }
        }
        [JwtAuthentication]
        [HttpDelete]
        [Route("deleteperiod")]
        /// <remarks>
        /// This is Delete api
        /// </remarks>
        /// <response code="200">Delete a period</response>
        /// <response code="404">period not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage Delete(String id)
        {
            try
            {
                periodManager.DeletePeriod(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Period deleted!");
            }

            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Delete:" + e.Message);
            }

        }
    }
}
