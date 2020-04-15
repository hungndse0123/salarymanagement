using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Salary_Management.Models
{
    public class SalaryTicketDetailModel
    {
        public String SalaryTicketID { get; set; }
        public String WorkingTimeID { get; set; }
        public String AllowanceID { get; set; }
        public String TaxID { get; set; }
        public int? WageTier { get; set; }
        public double? BasicSalary { get; set; }
        public double? FinalSalary { get; set; }


    }
}