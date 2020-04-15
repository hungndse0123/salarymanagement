using Api_Salary_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Salary_Management.Services
{
    interface IAllowance
    {
        IList<Allowance> GetAllowances(AllowanceModel allowance);
        bool CreateAllowance(Allowance allowance);
        bool UpdateAllowance(AllowanceModel allowance);
        bool DeleteAllowance(String id);
    }
}
