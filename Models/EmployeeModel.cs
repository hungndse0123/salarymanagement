using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Salary_Management.Models
{
    public class EmployeeModel
    {
        public string EmployeeID { get; set; }
        public String Password { get; set; }
        public String FullName { get; set; }
        public String Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public long? Phone { get; set; }
        public double? BasicSalary { get; set; }
        public String AllowanceID { get; set; }
        public int? WageTier { get; set; }
        public string TaxID { get; set; }
        public string Role { get; set; }
    }
}