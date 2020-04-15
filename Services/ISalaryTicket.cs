using Api_Salary_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Salary_Management.Services
{
    interface ISalaryTicket
    {
        IList<SalaryTicket> GetSalaryTickets(SalaryTicketModel ticket);
        bool CreateSalaryTicket(SalaryTicket salaryTicket);
        bool UpdateSalaryTicket(SalaryTicketModel salaryTicket);
        bool DeleteSalaryTicket(String id);
    }
}
