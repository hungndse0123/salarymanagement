using Api_Salary_Management.Models;
using Api_Salary_Management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Salary_Management.Respository
{
    public class AllowanceManager : IAllowance
    {
        SalaryManagement_SWD391_Project_Allowance_Entities _dbContext = new SalaryManagement_SWD391_Project_Allowance_Entities();
        public bool CreateAllowance(Allowance allowance)
        {
            _dbContext.Allowances.Add(new Allowance()
            {
                AllowanceID = allowance.AllowanceID,
                AllowanceDetail = allowance.AllowanceDetail,
                AllowanceMoney = allowance.AllowanceMoney
            });
            _dbContext.SaveChanges();
            return true;
        }

        public bool DeleteAllowance(string id)
        {
            var allowance = _dbContext.Allowances.Where(c => c.AllowanceID == id).FirstOrDefault();
            _dbContext.Entry(allowance).State = System.Data.Entity.EntityState.Deleted;
            _dbContext.SaveChanges();
            return true;
        }

        public IList<Allowance> GetAllowances(AllowanceModel allowance)
        {
            IList<Allowance> listallowance = null;
                if (allowance != null)
                {
                    listallowance = _dbContext.Allowances.Where(c => (c.AllowanceID.Contains(allowance.AllowanceID) || allowance.AllowanceID == null)
                                                        && (c.AllowanceDetail.Contains(allowance.AllowanceDetail) || allowance.AllowanceDetail == null)
                                                        && (c.AllowanceMoney.Contains(allowance.AllowanceMoney) || allowance.AllowanceMoney == null)
                                                        ).ToList<Allowance>();
                } else listallowance = _dbContext.Allowances.ToList<Allowance>();
            return listallowance;
        }

        public bool UpdateAllowance(AllowanceModel allowance)
        {
            var checkExistingAllowance = _dbContext.Allowances.Where(c => c.AllowanceID == allowance.AllowanceID).FirstOrDefault<Allowance>();
            if (checkExistingAllowance != null)
            {
                checkExistingAllowance.AllowanceDetail = allowance.AllowanceDetail;
                checkExistingAllowance.AllowanceMoney = allowance.AllowanceMoney;
                _dbContext.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}