using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Salary_Management.Models
{
    public class PeriodModel
    {
        public String PeriodID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? StandardWorkingDays { get; set; }
    }
}