using Api_Salary_Management.Respository;
using Api_Salary_Management.Services;
using Api_Salary_Management.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api_Salary_Management.Models;

namespace Api_Salary_Management.Controllers
{

    [RoutePrefix("api/token")]
    public class TokenController : ApiController
    {
        IToken tokenManager = new TokenManager(); 
        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        /// <remarks>
        /// This is Get api
        /// </remarks>
        /// <response code="200">If Username and password is valid, create a token to login</response>
        /// <response code="404">Username or password is invalid</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage Get(string username, string password)
        {
            if (tokenManager.CheckUser(username, password))
            {
                return Request.CreateResponse(HttpStatusCode.OK, JwtManager.GenerateToken(username));
            }
            else return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "There are no employee match the login value!!!");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("loginadmin")]
        /// <remarks>
        /// This is Get api
        /// </remarks>
        /// <response code="200">If Username and password and role is valid, create a token to login</response>
        /// <response code="404">Username or password is invalid</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage LoginAdmin(string username, string password)
        {
            if (tokenManager.CheckUser(username, password))
            {
                if (tokenManager.CheckAdmin(username, password)) return Request.CreateResponse(HttpStatusCode.OK, JwtManager.GenerateToken(username));
                else return Request.CreateResponse(HttpStatusCode.Unauthorized, "You are not admin!!! ");
            }
            else return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid Id or password!!!");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("validate")]
        /// <remarks>
        /// This is Get api
        /// </remarks>
        /// <response code="200">Valid Token</response>
        /// <response code="404">Invalid Token</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage Validate(TokenModel tokenModel)
        {
            if (tokenManager.ValidateToken(tokenModel.Token))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Valid Token!");
            }
            else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Token!");
        }
    }
}
