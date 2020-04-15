using Api_Salary_Management.Models;
using Api_Salary_Management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Salary_Management.Respository
{
    public class SalaryTicketDetailManager : ISalaryTicketDetail
    {
        public bool CalculateFinalSalary(SalaryTicketDetailModel salaryTicketDetail)
        {
            double? taxcoefficient = 1, allowanceMoney = 1, wagecoefficient = 1, workedtime = 1, standardworkingdays = 1, basicsalary = 1, finalsalary = 1;
            string periodId = "";
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_Tax())
            {
                var tax = ctx.PersonalIncomeTaxes.Where(c => c.TaxID == salaryTicketDetail.TaxID).FirstOrDefault<PersonalIncomeTax>();
                if (tax != null)
                {
                    taxcoefficient = tax.Coefficient;
                }
            }
            using (var ctx = new SalaryManagement_SWD391_Project_Allowance_Entities())
            {
                var allowance = ctx.Allowances.Where(c => c.AllowanceID == salaryTicketDetail.AllowanceID).FirstOrDefault<Allowance>();
                if (allowance != null)
                {
                    allowanceMoney = Double.Parse(allowance.AllowanceMoney);
                }
            }
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_Payroll())
            {
                var payroll = ctx.Payrolls.Where(c => c.WageTier == salaryTicketDetail.WageTier).FirstOrDefault<Payroll>();
                if (payroll != null)
                {
                    wagecoefficient = payroll.Coefficient;
                }
            }
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_WorkingTime())
            {
                var time = ctx.WorkingTimes.Where(c => c.WorkingTimeID == salaryTicketDetail.WorkingTimeID).FirstOrDefault<WorkingTime>();
                if (time != null)
                {
                    workedtime = time.WorkedTime;
                }
            }
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_SalaryTicket())
            {
                var ticket = ctx.SalaryTickets.Where(c => c.SalaryTicketID == salaryTicketDetail.SalaryTicketID).FirstOrDefault<SalaryTicket>();
                if (ticket != null)
                {
                    periodId = ticket.PeriodID;
                }
            }
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_Period())
            {
                var period = ctx.Periods.Where(c => c.PeriodID == periodId).FirstOrDefault<Period>();
                if (period != null)
                {
                    standardworkingdays = period.StandardWorkingDays;
                }
            }
            basicsalary = salaryTicketDetail.BasicSalary;
            finalsalary = ((basicsalary * wagecoefficient) * (workedtime / standardworkingdays)) - (basicsalary * taxcoefficient) + allowanceMoney;
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_SalaryTicketDetail())
            {
                var checkExistingTicketDetail = ctx.SalaryTicketDetails.Where(c => c.SalaryTicketID == salaryTicketDetail.SalaryTicketID).FirstOrDefault<SalaryTicketDetail>();
                if (checkExistingTicketDetail != null)
                {
                    checkExistingTicketDetail.FinalSalary = finalsalary;
                    ctx.SaveChanges();
                    return true;
                }
                else return false;
            }
        }

        public bool CreateSalaryTicketDetail(SalaryTicketDetail salaryTicketDetail)
        {
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_SalaryTicketDetail())
            {
                ctx.SalaryTicketDetails.Add(new SalaryTicketDetail()
                {
                    SalaryTicketID = salaryTicketDetail.SalaryTicketID,
                    AllowanceID = salaryTicketDetail.AllowanceID,
                    WorkingTimeID = salaryTicketDetail.WorkingTimeID,
                    TaxID = salaryTicketDetail.TaxID,
                    WageTier = salaryTicketDetail.WageTier,
                    BasicSalary = salaryTicketDetail.BasicSalary,
                    FinalSalary = salaryTicketDetail.FinalSalary
                });
                ctx.SaveChanges();
            }
            return true;
        }

        public bool DeleteSalaryTicketDetail(string id)
        {
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_SalaryTicketDetail())
            {
                var detail = ctx.SalaryTicketDetails.Where(c => c.SalaryTicketID == id).FirstOrDefault();
                ctx.Entry(detail).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
                return true;
            }
        }

        public SalaryTicketDetail GetExactSalaryTicketDetails(String id)
        {
            SalaryTicketDetail searcheddetail = null;
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_SalaryTicketDetail())
            {
                if (id != null)
                {
                    searcheddetail = ctx.SalaryTicketDetails.Where(c => (c.SalaryTicketID.Equals(id))
                                                        ).FirstOrDefault<SalaryTicketDetail>();
                }
            }
            return searcheddetail;
        }

        public IList<SalaryTicketDetail> GetSalaryTicketDetails(SalaryTicketDetailModel detail)
        {
            IList<SalaryTicketDetail> listdetail = null;
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_SalaryTicketDetail())
            {
                if (detail != null)
                {
                    listdetail = ctx.SalaryTicketDetails.Where(c => (c.SalaryTicketID.Contains(detail.SalaryTicketID) || detail.SalaryTicketID == null)
                                                        && (c.AllowanceID.Contains(detail.AllowanceID) || detail.AllowanceID == null)
                                                        && (c.TaxID.Contains(detail.TaxID) || detail.TaxID == null)
                                                        && (c.WorkingTimeID.Contains(detail.WorkingTimeID) || detail.WorkingTimeID == null)
                                                        && (c.WageTier == detail.WageTier || detail.WageTier == null)
                                                        && (c.BasicSalary == detail.BasicSalary || detail.BasicSalary == null)
                                                        && (c.FinalSalary == detail.FinalSalary || detail.FinalSalary == null)
                                                        ).ToList<SalaryTicketDetail>();
                } else listdetail = ctx.SalaryTicketDetails.ToList<SalaryTicketDetail>();
            }
            return listdetail;
        }

        public bool UpdateSalaryTicketDetail(SalaryTicketDetailModel salaryTicketDetail)
        {
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_SalaryTicketDetail())
            {
                var checkExistingTicketDetail = ctx.SalaryTicketDetails.Where(c => c.SalaryTicketID == salaryTicketDetail.SalaryTicketID).FirstOrDefault<SalaryTicketDetail>();
                if (checkExistingTicketDetail != null)
                {
                    checkExistingTicketDetail.TaxID = salaryTicketDetail.TaxID;
                    checkExistingTicketDetail.AllowanceID = salaryTicketDetail.AllowanceID;
                    checkExistingTicketDetail.WorkingTimeID = salaryTicketDetail.WorkingTimeID;
                    checkExistingTicketDetail.WageTier = (int)salaryTicketDetail.WageTier;
                    checkExistingTicketDetail.BasicSalary = (double)salaryTicketDetail.BasicSalary;
                    checkExistingTicketDetail.FinalSalary = (double)salaryTicketDetail.FinalSalary;
                    ctx.SaveChanges();
                    return true;
                }
                else return false;

            }
        }
    }
}