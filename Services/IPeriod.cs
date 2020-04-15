using Api_Salary_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Salary_Management.Services
{
    interface IPeriod
    {
        IList<Period> GetPeriods(PeriodModel period);
        bool CreatePeriod(Period period);
        bool UpdatePeriod(PeriodModel period);
        bool DeletePeriod(String id);
    }
}
