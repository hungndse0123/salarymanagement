using Api_Salary_Management.Models;
using Api_Salary_Management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Salary_Management.Respository
{
    public class PeriodManager : IPeriod
    {
        SalaryManagement_SWD391_ProjectEntities_Period ctx = new SalaryManagement_SWD391_ProjectEntities_Period();
        public bool CreatePeriod(Period period)
        {
            ctx.Periods.Add(new Period()
            {
                PeriodID = period.PeriodID,
                StartDate = period.StartDate,
                EndDate = period.EndDate,
                StandardWorkingDays = period.StandardWorkingDays
            });
            ctx.SaveChanges();
            return true;
        }

        public bool DeletePeriod(string id)
        {
            var period = ctx.Periods.Where(c => c.PeriodID == id).FirstOrDefault();
            ctx.Entry(period).State = System.Data.Entity.EntityState.Deleted;
            ctx.SaveChanges();
            return true;
        }

        public IList<Period> GetPeriods(PeriodModel period)
        {
            IList<Period> listperiod = null;
                if (period != null)
                {
                    listperiod = ctx.Periods.Where(c => (c.PeriodID.Contains(period.PeriodID) || period.PeriodID == null)
                                                        && (c.StartDate == period.StartDate || period.StartDate == null)
                                                        && (c.EndDate == period.EndDate || period.EndDate == null)
                                                        && (c.StandardWorkingDays == period.StandardWorkingDays || period.StandardWorkingDays == null)
                                                        ).ToList<Period>();
                } else listperiod = ctx.Periods.ToList<Period>();
            return listperiod;
        }

        public bool UpdatePeriod(PeriodModel period)
        {
            var checkExistingPeriod = ctx.Periods.Where(c => c.PeriodID == period.PeriodID).FirstOrDefault<Period>();
            if (checkExistingPeriod != null)
            {
                checkExistingPeriod.StartDate = (DateTime)period.StartDate;
                checkExistingPeriod.EndDate = (DateTime)period.EndDate;
                checkExistingPeriod.StandardWorkingDays = (Double)period.StandardWorkingDays;
                ctx.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}