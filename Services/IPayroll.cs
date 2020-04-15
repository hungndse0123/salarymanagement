using Api_Salary_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Salary_Management.Services
{
    interface IPayroll
    {
        IList<Payroll> GetPayrolls(PayrollModel payroll);
        bool CreatePayroll(Payroll payroll);
        bool UpdatePayroll(PayrollModel payroll);
        bool DeletePayroll(int id);
    }
}
