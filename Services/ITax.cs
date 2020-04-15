using Api_Salary_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Salary_Management.Services
{
    interface ITax
    {
        IList<PersonalIncomeTax> GetTaxs(PersonalIncomeTaxModel tax);
        bool CreateTax(PersonalIncomeTax personalIncomeTax);
        bool UpdateTax(PersonalIncomeTaxModel personalIncomeTax);
        bool DeleteTax(String id);
    }
}
