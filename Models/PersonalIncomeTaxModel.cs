using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Salary_Management.Models
{
    public class PersonalIncomeTaxModel
    {
        public String TaxID { get; set; }
        public String TaxDetail { get; set; }
        public double? Coefficient { get; set; }
    }
}