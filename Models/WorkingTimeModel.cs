using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Salary_Management.Models
{
    public class WorkingTimeModel
    {
        public String WorkingTimeID { get; set; }
        public String EmployeeID { get; set; }
        public String PeriodID { get; set; }
        public double? WorkedTime { get; set; }
    }
}