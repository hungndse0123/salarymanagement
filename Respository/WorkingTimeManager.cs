using Api_Salary_Management.Models;
using Api_Salary_Management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Salary_Management.Respository
{
    public class WorkingTimeManager : IWorkingTime
    {
        public bool CreateWorkingTime(WorkingTime workingTime)
        {
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_WorkingTime())
            {
                ctx.WorkingTimes.Add(new WorkingTime()
                {
                    WorkingTimeID = workingTime.WorkingTimeID,
                    EmployeeID = workingTime.EmployeeID,
                    PeriodID = workingTime.PeriodID,
                    WorkedTime = workingTime.WorkedTime
                });
                ctx.SaveChanges();
                return true;
            }
        }

        public bool DeleteWorkingTime(string id)
        {
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_WorkingTime())
            {
                var time = ctx.WorkingTimes.Where(c => c.WorkingTimeID == id).FirstOrDefault();
                ctx.Entry(time).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
                return true;
            }
        }

        public IList<WorkingTime> GetWorkingTimes(WorkingTimeModel workingTime)
        {
            IList<WorkingTime> listtime = null;
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_WorkingTime())
            {
                if (workingTime != null)
                {
                    listtime = ctx.WorkingTimes.Where(c => (c.WorkingTimeID.Contains(workingTime.WorkingTimeID) || workingTime.WorkingTimeID == null)
                                                        && (c.EmployeeID.Contains(workingTime.EmployeeID) || workingTime.EmployeeID == null)
                                                        && (c.PeriodID.Contains(workingTime.PeriodID) || workingTime.PeriodID == null)
                                                        && (c.WorkedTime == workingTime.WorkedTime || workingTime.WorkedTime == null)
                                                        ).ToList<WorkingTime>();
                } else listtime = ctx.WorkingTimes.ToList<WorkingTime>();
            }
            return listtime;
        }

        public bool UpdateWorkingTime(WorkingTimeModel workingTime)
        {
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_WorkingTime())
            {
                var checkExistingWorkingTime = ctx.WorkingTimes.Where(c => c.WorkingTimeID == workingTime.WorkingTimeID).FirstOrDefault<WorkingTime>();
                if (checkExistingWorkingTime != null)
                {
                    checkExistingWorkingTime.EmployeeID = workingTime.EmployeeID;
                    checkExistingWorkingTime.PeriodID = workingTime.PeriodID;
                    checkExistingWorkingTime.WorkedTime = (double)workingTime.WorkedTime;
                    ctx.SaveChanges();
                    return true;
                }
                else return false;

            }
        }
    }
}