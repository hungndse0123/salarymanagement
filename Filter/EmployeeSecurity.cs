using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Salary_Management
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password)
        {
            using (SalaryManagement_SWD391_ProjectEntities_Employee entities = new SalaryManagement_SWD391_ProjectEntities_Employee())
            {
                return entities.Employees.Any(user =>
                       user.EmployeeID.Equals(username, StringComparison.OrdinalIgnoreCase)
                                          && user.Password == password);
            }
        }
    }
}