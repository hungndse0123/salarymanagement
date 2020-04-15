using Api_Salary_Management.Models;
using Api_Salary_Management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Salary_Management.Respository
{
    public class EmployeeManager : IEmployee
    {
        SalaryManagement_SWD391_ProjectEntities_Employee ctx = new SalaryManagement_SWD391_ProjectEntities_Employee();
        public bool CreateEmployee(Employee employee)
        {
                ctx.Employees.Add(new Employee()
                {
                    EmployeeID = employee.EmployeeID,
                    Password = employee.Password,
                    FullName = employee.FullName,
                    Gender = employee.Gender,
                    Birthday = employee.Birthday,
                    Address = employee.Address,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    BasicSalary = employee.BasicSalary,
                    AllowanceID = employee.AllowanceID,
                    WageTier = employee.WageTier,
                    TaxID = employee.TaxID,
                    Role = employee.Role
                });
                ctx.SaveChanges();
            return true;
        }

        public bool DeleteEmployee(string id)
        {
            var employee = ctx.Employees.Where(c => c.EmployeeID == id).FirstOrDefault();
            ctx.Entry(employee).State = System.Data.Entity.EntityState.Deleted;
            ctx.SaveChanges();
            return true;
        }

        public IList<Employee> GetEmployees(string fullname, string id, int tier)
        {
            IList<Employee> listemployee = null;
                    listemployee = ctx.Employees.Where(c => (c.FullName.Contains(fullname) || fullname == null)
                                                        && (c.EmployeeID.Contains(id) || id == null)
                                                        && (c.WageTier == tier || tier == -1)
                                                        ).ToList<Employee>();
            return listemployee;
        }

        public Employee GetExactEmployees(string id)
        {
            Employee employee = null;
            if (id != null )
            {
                employee = ctx.Employees.Where(c => (c.EmployeeID.Equals(id))
                                                ).FirstOrDefault<Employee>();
            }      
            return employee;
        }

        public bool UpdateEmployee(EmployeeModel employee)
        {
            var checkExistingEmployee = ctx.Employees.Where(c => c.EmployeeID == employee.EmployeeID).FirstOrDefault<Employee>();
            if (checkExistingEmployee != null)
            {
                checkExistingEmployee.Password = employee.Password;
                checkExistingEmployee.FullName = employee.FullName;
                checkExistingEmployee.Gender = employee.Gender;
                checkExistingEmployee.Birthday = (DateTime)employee.Birthday;
                checkExistingEmployee.Address = employee.Address;
                checkExistingEmployee.Email = employee.Email;
                checkExistingEmployee.Phone = (long)employee.Phone;
                checkExistingEmployee.BasicSalary = (Double)employee.BasicSalary;
                checkExistingEmployee.AllowanceID = employee.AllowanceID;
                checkExistingEmployee.WageTier = (Int32)employee.WageTier;
                checkExistingEmployee.TaxID = employee.TaxID;
                checkExistingEmployee.Role = employee.Role;
                ctx.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}