using Api_Salary_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Salary_Management.Services
{
    interface IWorkingTime
    {
        IList<WorkingTime> GetWorkingTimes(WorkingTimeModel workingTime);
        bool CreateWorkingTime(WorkingTime workingTime);
        bool UpdateWorkingTime(WorkingTimeModel workingTime);
        bool DeleteWorkingTime(String id);
    }
}
