using Api_Salary_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Salary_Management.Services
{
    interface ISalaryTicketDetail
    {
        IList<SalaryTicketDetail> GetSalaryTicketDetails(SalaryTicketDetailModel detail);
        SalaryTicketDetail GetExactSalaryTicketDetails(String id);
        bool CreateSalaryTicketDetail(SalaryTicketDetail salaryTicketDetail);
        bool UpdateSalaryTicketDetail(SalaryTicketDetailModel salaryTicketDetail);
        bool CalculateFinalSalary(SalaryTicketDetailModel salaryTicketDetail);
        bool DeleteSalaryTicketDetail(String id);
    }
}
