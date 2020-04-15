using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Salary_Management.Services
{
    interface IToken
    {
        bool CheckUser(string username, string password);
        bool CheckAdmin(string username, string password);
        bool ValidateToken(string token);
    }
}
