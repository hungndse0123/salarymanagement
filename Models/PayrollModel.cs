using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Salary_Management.Models
{
    public class PayrollModel
    {
        public int? WageTier { get; set; }
        public double? Coefficient { get; set; }
    }
}