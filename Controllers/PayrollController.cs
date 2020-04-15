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
    [RoutePrefix("api/payroll")]
    public class PayrollController : ApiController
    {
        IPayroll payrollManager = new PayrollManager();
        [JwtAuthentication]
        [HttpGet]
        [Route("getpayroll")]
        /// <remarks>
        /// This is Get api
        /// </remarks>
        /// <response code="200">Returns the list of payroll</response>
        /// <response code="404">payroll not find</response>
        [System.Web.Http.Description.ResponseType(typeof(IList<PayrollModel>))]
        public HttpResponseMessage GetPayroll([FromUri] PayrollModel payroll)
        {
            try
            {
                IList<Payroll> listpayroll = null;
                listpayroll = payrollManager.GetPayrolls(payroll);
                if (listpayroll == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no payroll's name match the search value!!!");
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { PayrollModel = listpayroll });
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing GetPayroll:" + e.Message);
            }
        }

        [JwtAuthentication]
        [HttpPost]
        [Route("createpayroll")]
        /// <remarks>
        /// This is Post api
        /// </remarks>
        /// <response code="200">Create a payroll</response>
        /// <response code="404">payroll not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PostNewPayroll(Payroll payroll)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");
                payrollManager.CreatePayroll(payroll);
                return Request.CreateResponse(HttpStatusCode.OK, "Payroll created!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing PostNewPayroll:" + e.Message);
            }
        }
        [JwtAuthentication]
        [HttpPut]
        [Route("updatepayroll")]
        /// <remarks>
        /// This is Put api
        /// </remarks>
        /// <response code="200">Create a payroll</response>
        /// <response code="404">payroll not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PutPayroll(PayrollModel payroll)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");
                bool check = payrollManager.UpdatePayroll(payroll);
                if (check) return Request.CreateResponse(HttpStatusCode.OK, "Payroll updated!");
                else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Payroll not found!!!");  
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Put:" + e.Message);
            }
        }

        [JwtAuthentication]
        [HttpDelete]
        [Route("deletepayroll")]
        /// <remarks>
        /// This is Delete api
        /// </remarks>
        /// <response code="200">Delete a payroll</response>
        /// <response code="404">payroll not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                payrollManager.DeletePayroll(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Payroll deleted!");
            }

            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Delete:" + e.Message);
            }

        }
    }
}
