using Api_Salary_Management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Api_Salary_Management.Respository
{
    public class TokenManager : IToken
    {
        public bool CheckAdmin(string username, string password)
        {
            try
            {
                Employee listemployee = null;
                using (var ctx = new SalaryManagement_SWD391_ProjectEntities_Employee())
                {
                    if (username != null && password != null)
                    {
                        listemployee = ctx.Employees.Where(c => (c.EmployeeID.Equals(username) && c.Password.Equals(password) && c.Role.Equals("Admin"))
                                                            ).FirstOrDefault<Employee>();
                    }
                }
                if (listemployee == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool CheckUser(string username, string password)
        {
            try
            {
                Employee listemployee = null;
                using (var ctx = new SalaryManagement_SWD391_ProjectEntities_Employee())
                {
                    if (username != null && password != null)
                    {
                        listemployee = ctx.Employees.Where(c => (c.EmployeeID.Equals(username) && c.Password.Equals(password))
                                                            ).FirstOrDefault<Employee>();
                    }
                }
                if (listemployee == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ValidateToken(string token)
        {
            String username = null;

            var simplePrinciple = JwtManager.GetPrincipal(token);
            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim?.Value;

            if (string.IsNullOrEmpty(username))
                return false;

            // More validate to check whether username exists in system

            return true;
        }

    }
}