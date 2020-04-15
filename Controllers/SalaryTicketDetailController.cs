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
    [RoutePrefix("api/salaryticketdetail")]
    public class SalaryTicketDetailController : ApiController
    {
        ISalaryTicketDetail salaryTicketDetailManager = new SalaryTicketDetailManager();
        [JwtAuthentication]
        [HttpGet]
        [Route("getdetail")]
        /// <remarks>
        /// This is Get api
        /// </remarks>
        /// <response code="200">Returns the list of salary ticket detail</response>
        /// <response code="404">salary ticket detail not find</response>
        [System.Web.Http.Description.ResponseType(typeof(IList<SalaryTicketDetailModel>))]
        public HttpResponseMessage GetSalaryTicketDetail([FromUri] SalaryTicketDetailModel detail)
        {
            try
            {
                IList<SalaryTicketDetail> listdetail = null;
                listdetail = salaryTicketDetailManager.GetSalaryTicketDetails(detail);
                if (listdetail == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no ticket detail's name match the search value!!!");
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { SalaryTicketDetailModel = listdetail });
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing GetSalaryTicketDetail:" + e.Message);
            }
        }

        [JwtAuthentication]
        [HttpGet]
        [Route("getexactdetail")]
        /// <remarks>
        /// This is Get api
        /// </remarks>
        /// <response code="200">Returns the searched salary ticket detail</response>
        /// <response code="404">salary ticket detail not find</response>
        [System.Web.Http.Description.ResponseType(typeof(SalaryTicketDetailModel))]
        public HttpResponseMessage GetExactSalaryTicketDetail([FromUri] String id = null)
        {
            try
            {
                SalaryTicketDetail searcheddetail = null;
                searcheddetail = salaryTicketDetailManager.GetExactSalaryTicketDetails(id);
                if (searcheddetail == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no ticket detail's name match the search value!!!");
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { SalaryTicketDetailModel = searcheddetail });
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing GetSalaryTicketDetail:" + e.Message);
            }
        }

        [JwtAuthentication]
        [HttpPost]
        [Route("createdetail")]
        /// <remarks>
        /// This is Post api
        /// </remarks>
        /// <response code="200">Create a salary ticket detail</response>
        /// <response code="404">salary ticket detail not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PostNewTicketDetail(SalaryTicketDetail salaryTicketDetail)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");
                salaryTicketDetailManager.CreateSalaryTicketDetail(salaryTicketDetail);
                return Request.CreateResponse(HttpStatusCode.OK, "Ticket's detail created!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing PostNewTicketDetail:" + e.Message);
            }
        }
        [HttpPut]
        [JwtAuthentication]
        /// <remarks>
        /// This is Put api
        /// </remarks>
        /// <response code="200">Update a salary ticket detail</response>
        /// <response code="404">salary ticket detail not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]    
        [Route("updatedetail")]
        public HttpResponseMessage PutTicketDetail(SalaryTicketDetailModel salaryTicketDetail)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");
                bool check = salaryTicketDetailManager.UpdateSalaryTicketDetail(salaryTicketDetail);
                if (check) return Request.CreateResponse(HttpStatusCode.OK, "Ticket's detail updated!");
                else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Ticket's detail not found!!!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Put:" + e.Message);
            }
        }
        [JwtAuthentication]
        [HttpDelete]
        [Route("deletetax")]
        /// <remarks>
        /// This is Delete api
        /// </remarks>
        /// <response code="200">Delete a salary ticket detail</response>
        /// <response code="404">salary ticket detail not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage Delete(String id)
        {
            try
            {
                salaryTicketDetailManager.DeleteSalaryTicketDetail(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Ticket's detail deleted!");
            }

            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Delete:" + e.Message);
            }

        }

        /// <remarks>
        /// This is Put api
        /// </remarks>
        /// <response code="200">Calculate final salary</response>
        /// <response code="404">salary ticket detail not valid</response>
        [HttpPut]
        [Route("calculatefinal")]
        [JwtAuthentication]
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PutFinalSalary(SalaryTicketDetailModel salaryTicketDetail)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");
                bool check = salaryTicketDetailManager.CalculateFinalSalary(salaryTicketDetail);
                if (check) return Request.CreateResponse(HttpStatusCode.OK, "Ticket's detail updated!");
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Ticket's detail not found!!!");                
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Put:" + e.Message);
            }
        }

    }
}
