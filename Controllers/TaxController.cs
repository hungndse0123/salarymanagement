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
    [RoutePrefix("api/tax")]
    
    public class TaxController : ApiController
    {
        ITax taxManager = new TaxManager();
        [JwtAuthentication]
        [HttpGet]
        [Route("gettax")]
        /// <remarks>
        /// This is Get api
        /// </remarks>
        /// <response code="200">Returns the list of tax</response>
        /// <response code="404">tax not find</response>
        [System.Web.Http.Description.ResponseType(typeof(IList<PersonalIncomeTaxModel>))]
        public HttpResponseMessage GetTax([FromUri] PersonalIncomeTaxModel tax)
        {
            try
            {
                IList<PersonalIncomeTax> listtax = null;
                listtax = taxManager.GetTaxs(tax);
                if (listtax == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no tax's name match the search value!!!");
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { PersonalIncomeTaxModel = listtax });
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing GetTax:" + e.Message);
            }
        }

        [JwtAuthentication]
        [HttpPost]
        [Route("createtax")]
        /// <remarks>
        /// This is Post api
        /// </remarks>
        /// <response code="200">Create a tax</response>
        /// <response code="404">tax not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PostNewTax(PersonalIncomeTax personalIncomeTax)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");
                taxManager.CreateTax(personalIncomeTax);
                return Request.CreateResponse(HttpStatusCode.OK, "Tax created!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing PostNewTax:" + e.Message);
            }
        }
        [JwtAuthentication]
        [HttpPut]
        [Route("updatetax")]
        /// <remarks>
        /// This is Put api
        /// </remarks>
        /// <response code="200">Update a tax</response>
        /// <response code="404">tax not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PutTax(PersonalIncomeTaxModel personalIncomeTax)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");
                bool check = taxManager.UpdateTax(personalIncomeTax);
                if (check) return Request.CreateResponse(HttpStatusCode.OK, "Tax updated!");
                else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Tax not found!!!");
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
        /// <response code="200">Delete a tax</response>
        /// <response code="404">tax not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage Delete(String id)
        {
            try
            {
                taxManager.DeleteTax(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Tax deleted!");
            }

            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Delete:" + e.Message);
            }

        }
    }
}
