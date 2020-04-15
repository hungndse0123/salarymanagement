using Api_Salary_Management.Models;
using Api_Salary_Management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Salary_Management.Respository
{
    public class SalaryTicketManager : ISalaryTicket
    {
        SalaryManagement_SWD391_ProjectEntities_SalaryTicket ctx = new SalaryManagement_SWD391_ProjectEntities_SalaryTicket();
        public bool CreateSalaryTicket(SalaryTicket salaryTicket)
        {
            ctx.SalaryTickets.Add(new SalaryTicket()
            {
                EmployeeID = salaryTicket.EmployeeID,
                SalaryTicketID = salaryTicket.SalaryTicketID,
                PeriodID = salaryTicket.PeriodID
            });
            ctx.SaveChanges();
            return true;
        }

        public bool DeleteSalaryTicket(string id)
        {
            var ticket = ctx.SalaryTickets.Where(c => c.SalaryTicketID == id).FirstOrDefault();
            ctx.Entry(ticket).State = System.Data.Entity.EntityState.Deleted;
            ctx.SaveChanges();
            return true;
        }

        public IList<SalaryTicket> GetSalaryTickets(SalaryTicketModel ticket)
        {
            IList<SalaryTicket> listticket = null;
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_SalaryTicket())
            {
                if (ticket != null)
                {
                    listticket = ctx.SalaryTickets.Where(c => (c.EmployeeID.Contains(ticket.EmployeeID) || ticket.EmployeeID == null)
                                                        && (c.PeriodID.Contains(ticket.PeriodID) || ticket.PeriodID == null)
                                                        && (c.SalaryTicketID.Contains(ticket.SalaryTicketID) || ticket.SalaryTicketID == null)
                                                        ).ToList<SalaryTicket>();
                }
                else listticket = ctx.SalaryTickets.ToList<SalaryTicket>();
            }
            return listticket;
        }

        public bool UpdateSalaryTicket(SalaryTicketModel salaryTicket)
        {
            var checkExistingTicket = ctx.SalaryTickets.Where(c => c.SalaryTicketID == salaryTicket.SalaryTicketID).FirstOrDefault<SalaryTicket>();
            if (checkExistingTicket != null)
            {
                checkExistingTicket.EmployeeID = salaryTicket.EmployeeID;
                checkExistingTicket.PeriodID = salaryTicket.PeriodID;
                ctx.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}