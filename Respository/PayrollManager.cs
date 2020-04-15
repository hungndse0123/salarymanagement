using Api_Salary_Management.Models;
using Api_Salary_Management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Salary_Management.Respository
{
    public class PayrollManager : IPayroll
    {
        SalaryManagement_SWD391_ProjectEntities_Payroll ctx = new SalaryManagement_SWD391_ProjectEntities_Payroll();
        public bool CreatePayroll(Payroll payroll)
        {
            ctx.Payrolls.Add(new Payroll()
            {
                Coefficient = payroll.Coefficient,
                WageTier = payroll.WageTier,
            });
            ctx.SaveChanges();
            return true;
        }

        public bool DeletePayroll(int id)
        {
            var payroll = ctx.Payrolls.Where(c => c.WageTier == id).FirstOrDefault();
            ctx.Entry(payroll).State = System.Data.Entity.EntityState.Deleted;
            ctx.SaveChanges();
            return true;
        }

        public IList<Payroll> GetPayrolls(PayrollModel payroll)
        {
            IList<Payroll> listpayroll = null;
                if (payroll != null)
                {
                    listpayroll = ctx.Payrolls.Where(c => (c.Coefficient == payroll.Coefficient || payroll.Coefficient == null)
                                                        && (c.WageTier == payroll.WageTier || payroll.WageTier == null)
                                                        ).ToList<Payroll>();
                } else listpayroll = ctx.Payrolls.ToList<Payroll>();
            return listpayroll;
        }

        public bool UpdatePayroll(PayrollModel payroll)
        {
            var checkExistingPayroll = ctx.Payrolls.Where(c => c.WageTier == payroll.WageTier).FirstOrDefault<Payroll>();
            if (checkExistingPayroll != null)
            {
                checkExistingPayroll.Coefficient = (double)payroll.Coefficient;
                ctx.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}