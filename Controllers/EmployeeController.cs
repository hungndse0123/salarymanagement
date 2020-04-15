using Api_Salary_Management.Filter;
using Api_Salary_Management.Models;
using Api_Salary_Management.Respository;
using Api_Salary_Management.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace Api_Salary_Management.Controllers
{
    [RoutePrefix("api/employee")]
    
    public class EmployeeController : ApiController
    {
        IEmployee employeeManager = new EmployeeManager();
        [JwtAuthentication]
        [HttpGet]
        [Route("getemployee")]
        /// <remarks>
        /// This is Get api
        /// </remarks>
        /// <response code="200">Returns the list of employee</response>
        /// <response code="404">employee not find</response>
        [System.Web.Http.Description.ResponseType(typeof(IList<EmployeeModel>))]
        public HttpResponseMessage GetEmployee([FromUri] string fullname=null, string id=null, int? tier=-1)
        {
            try
            {
                IList<Employee> listemployee = null;
                listemployee = employeeManager.GetEmployees(fullname,id,(int)tier);
                if (listemployee == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no employee's name match the search value!!!");
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { EmployeeModel = listemployee });
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing GetEmployee:" + e.Message);
            }
        }

        [JwtAuthentication]
        [HttpGet]
        [Route("getexactemployee")]
        /// <remarks>
        /// This is Get api
        /// </remarks>
        /// <response code="200">Returns the searched employee</response>
        /// <response code="404">employee not find</response>
        [System.Web.Http.Description.ResponseType(typeof(EmployeeModel))]
        public HttpResponseMessage GetExactEmployee([FromUri] string id = null)
        {
            try
            {
                Employee employee = null;
                employee = employeeManager.GetExactEmployees(id);
                if (employee == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no employee's name match the search value!!!");
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { EmployeeModel = employee });
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing GetEmployee:" + e.Message);
            }
        }

        //[NonAction]
        //public HttpResponseMessage GetAllEmployee()
        //{
        //    try
        //    {
        //        IList<EmployeeModel> employee = null;
        //        using (var ctx = new SalaryManagement_SWD391_ProjectEntities())
        //        {
        //            employee = ctx.Employees.Select(c => new EmployeeModel()
        //            {
        //                EmployeeID = c.EmployeeID,
        //                Password = c.Password,
        //                FullName = c.FullName,
        //                Gender = c.Gender,
        //                Birthday = c.Birthday,
        //                Address = c.Address,
        //                Email = c.Email,
        //                Phone = c.Phone,
        //                BasicSalary = c.BasicSalary,
        //                AllowanceID = c.AllowanceID,
        //                WageTier = c.WageTier,
        //                TaxID = c.TaxID
        //            }).ToList<EmployeeModel>();
        //            if (employee.Count == 0)
        //            {
        //                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no employee in database");
        //            }
        //            return Request.CreateResponse(HttpStatusCode.OK, new { Employee = employee });

        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing GetAllUser:" + e.Message);
        //    }
        //}
        //[HttpGet]
        //[Route("search-by-name/{id}")]
        //public HttpResponseMessage GetEmployeeByName(string id)
        //{
        //    try
        //    {
        //        IList<Employee> employee = null;
        //        using (var ctx = new SalaryManagement_SWD391_ProjectEntities())
        //        {
        //            if (!string.IsNullOrEmpty(id))
        //            {
        //                employee = ctx.Employees.Where(c => c.FullName.Contains(id)).ToList<Employee>();
        //            }
        //        }
        //        if (employee == null)
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no employee's name match the search value!!!");
        //        }

        //        return Request.CreateResponse(HttpStatusCode.OK, new { EmployeeModel = employee });
        //    }
        //    catch (Exception e)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing GetEmployeeByName:" + e.Message);
        //    }
        //}

        //[HttpGet]
        //[Route("search-by-wage/{id}")]
        //public HttpResponseMessage GetProductsByWageTier(string id)
        //{
        //    if (Regex.IsMatch(id, "^[0-9]+$"))
        //    {
        //        IList<Employee> employee = null;
        //        using (var ctx = new SalaryManagement_SWD391_ProjectEntities())
        //        {
        //            int Intid = Int32.Parse(id);
        //            employee = ctx.Employees.Where(s => s.WageTier == Intid).ToList<Employee>();

        //        }


        //        if (employee == null)
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no employee match the Wage Tier!!!");
        //        }

        //        return Request.CreateResponse(HttpStatusCode.OK, new { EmployeeModel = employee });
        //    }
        //    else
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "the wage tier is invalid!");
        //    }
        //}
        [JwtAuthentication]
        [HttpPost]
        [Route("createemployee")]
        /// <remarks>
        /// This is Post api
        /// </remarks>
        /// <response code="200">Create an employee</response>
        /// <response code="404">employee not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PostNewEmployee(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");
                employeeManager.CreateEmployee(employee);
                return Request.CreateResponse(HttpStatusCode.OK, "Employee created!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing PostNewEmployee:" + e.Message);
            }
        }
        [JwtAuthentication]
        [HttpPut]
        [Route("updateemployee")]
        /// <remarks>
        /// This is Put api
        /// </remarks>
        /// <response code="200">Update an employee</response>
        /// <response code="404">employee not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PutEmployee(EmployeeModel employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data!!!");
                bool check = employeeManager.UpdateEmployee(employee);
                if (check) return Request.CreateResponse(HttpStatusCode.OK, "Employee updated!");
                else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee not found!!!");    
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Put:" + e.Message);
            }
        }
        [JwtAuthentication]
        [HttpDelete]
        [Route("deleteemployee")]
        /// <remarks>
        /// This is Delete api
        /// </remarks>
        /// <response code="200">Delete an employee</response>
        /// <response code="404">employee not find</response>
        [System.Web.Http.Description.ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage Delete(String id)
        {
            try
            {
                employeeManager.DeleteEmployee(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Employee deleted!");
            }

            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing Delete:" + e.Message);
            }

        }
       
        
    }
}
