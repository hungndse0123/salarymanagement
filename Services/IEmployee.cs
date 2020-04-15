using Api_Salary_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Salary_Management.Services
{
    interface IEmployee
    {
        IList<Employee> GetEmployees(string fullname, string id, int tier);
        Employee GetExactEmployees(string id);
        bool CreateEmployee(Employee employee);
        bool UpdateEmployee(EmployeeModel employee);
        bool DeleteEmployee(String id);
    }
}
