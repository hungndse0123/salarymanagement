using Api_Salary_Management.Filter;
using Api_Salary_Management.Models;
using Api_Salary_Management.Respository;
using Api_Salary_Management.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Api_Salary_Management.Controllers
{
    [RoutePrefix("api/salaryticket")]
    
    public class SalaryTicketController : ApiController
    {
        ISalaryTicket salaryTicketManager = new SalaryTicketManager();
        [JwtAuthentication]
        [HttpGet]
        [Route("getticket")]
        /// <remarks>
        /// This is Get api
        /// </remarks>
        /// <response code="200">Returns the list of salary ticket</response>
        /// <response code="404">ticket not find</response>
        [System.Web.Http.Description.ResponseType(typeof(IList<SalaryTicketModel>))]
        public HttpResponseMessage GetSalaryTicket([FromUri] SalaryTicketModel ticket)
        {
            try
            {
                IList<SalaryTicket> listticket = null;
                listticket = salaryTicketManager.GetSalaryTickets(ticket);
                if (listticket == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no ticket's name match the search value!!!");
                }
                
                return Request.CreateResponse(HttpStatusCode.OK, new { SalaryTicketModel = listticket });
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing GetSalaryTicket:" + e.Message);
            }
        }

        [JwtAuthentication]
        [HttpPost]
        [Route("createticket")]
        /// <remarks>
        /// This is Post api
        /// </remarks>
        /// <response code="200">Create a salary ticket</response>
        /// <response code="404">salary ticket not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PostNewTicket(SalaryTicket salaryTicket)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");
                salaryTicketManager.CreateSalaryTicket(salaryTicket);
                return Request.CreateResponse(HttpStatusCode.OK, "Ticket created!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing PostNewTicket:" + e.Message);
            }
        }
        [JwtAuthentication]
        [HttpPut]
        [Route("updateticket")]
        /// <remarks>
        /// This is Put api
        /// </remarks>
        /// <response code="200">Update a salary ticket</response>
        /// <response code="404">salary ticket not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PutTicket(SalaryTicketModel salaryTicket)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");
                bool check = salaryTicketManager.UpdateSalaryTicket(salaryTicket);
                if (check) return Request.CreateResponse(HttpStatusCode.OK, "Ticket updated!");
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Ticket not found!!!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Put:" + e.Message);
            }
        }
        [JwtAuthentication]
        [HttpDelete]
        [Route("deleteticket")]
        /// <remarks>
        /// This is Delete api
        /// </remarks>
        /// <response code="200">Delete a salary ticket</response>
        /// <response code="404">salary ticket not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage Delete(String id)
        {
            try
            {
                salaryTicketManager.DeleteSalaryTicket(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Ticket deleted!");
            }

            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Delete:" + e.Message);
            }

        }
    }
}
