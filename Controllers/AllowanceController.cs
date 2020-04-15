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
    [RoutePrefix("api/allowance")]
    
    public class AllowanceController : ApiController
    {
        IAllowance allowanceManager = new AllowanceManager();
        [JwtAuthentication]
        [HttpGet]
        [Route("getallowance")]
        /// <remarks>
        /// This is Get api
        /// </remarks>
        /// <response code="200">Returns the list of allowance</response>
        /// <response code="404">allowance not find</response>
        [System.Web.Http.Description.ResponseType(typeof(IList<AllowanceModel>))]
        public HttpResponseMessage GetAllowance([FromUri] AllowanceModel allowance)
        {
            try
            {
                IList<Allowance> listallowance = null;
                listallowance = allowanceManager.GetAllowances(allowance);
                if (listallowance == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no allowance's name match the search value!!!");
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { AllowanceModel = listallowance });
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing GetAllowance:" + e.Message);
            }
        }

        [JwtAuthentication]
        [HttpPost]
        [Route("createallowance")]
        /// <remarks>
        /// This is Post api
        /// </remarks>
        /// <response code="200">Create an allowance</response>
        /// <response code="404">allowance not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PostNewAllowance(Allowance allowance)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");
                allowanceManager.CreateAllowance(allowance);
                return Request.CreateResponse(HttpStatusCode.OK, "Allowance created!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing PostNewAllowance:" + e.Message);
            }
        }
        [JwtAuthentication]
        [HttpPut]
        [Route("updateallowance")]
        /// <remarks>
        /// This is Put api
        /// </remarks>
        /// <response code="200">Update an allowance</response>
        /// <response code="404">allowance not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PutAllowance(AllowanceModel allowance)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");   
                bool check = allowanceManager.UpdateAllowance(allowance);
                if (check) return Request.CreateResponse(HttpStatusCode.OK, "Allowance updated!");
                else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Allowance not found!!!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Put:" + e.Message);
            }
        }
        [JwtAuthentication]
        [HttpDelete]
        [Route("deleteallowance")]
        /// <remarks>
        /// This is Delete api
        /// </remarks>
        /// <response code="200">Delete an allowance</response>
        /// <response code="404">allowance not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage Delete(String id)
        {
            try
            {
                allowanceManager.DeleteAllowance(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Allowance deleted!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Delete:" + e.Message);
            }

        }
    }
}
