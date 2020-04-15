using Api_Salary_Management.Models;
using Api_Salary_Management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Salary_Management.Respository
{
    public class TaxManager : ITax
    {
        public bool CreateTax(PersonalIncomeTax personalIncomeTax)
        {
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_Tax())
            {
                ctx.PersonalIncomeTaxes.Add(new PersonalIncomeTax()
                {
                    TaxID = personalIncomeTax.TaxID,
                    TaxDetail = personalIncomeTax.TaxDetail,
                    Coefficient = personalIncomeTax.Coefficient
                });
                ctx.SaveChanges();
            }
            return true;
        }

        public bool DeleteTax(string id)
        {
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_Tax())
            {
                var tax = ctx.PersonalIncomeTaxes.Where(c => c.TaxID == id).FirstOrDefault();
                ctx.Entry(tax).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
                return true;
            }
        }

        public IList<PersonalIncomeTax> GetTaxs(PersonalIncomeTaxModel tax)
        {
            IList<PersonalIncomeTax> listtax = null;
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_Tax())
            {
                if (tax != null)
                {
                    listtax = ctx.PersonalIncomeTaxes.Where(c => (c.TaxID.Contains(tax.TaxID) || tax.TaxID == null)
                                                        && (c.TaxDetail.Contains(tax.TaxDetail) || tax.TaxDetail == null)
                                                        && (c.Coefficient == tax.Coefficient || tax.Coefficient == null)
                                                        ).ToList<PersonalIncomeTax>();
                } else listtax = ctx.PersonalIncomeTaxes.ToList<PersonalIncomeTax>();
            } 
            return listtax;
        }

        public bool UpdateTax(PersonalIncomeTaxModel personalIncomeTax)
        {
            using (var ctx = new SalaryManagement_SWD391_ProjectEntities_Tax())
            {
                var checkExistingTax = ctx.PersonalIncomeTaxes.Where(c => c.TaxID == personalIncomeTax.TaxID).FirstOrDefault<PersonalIncomeTax>();
                if (checkExistingTax != null)
                {
                    checkExistingTax.TaxDetail = personalIncomeTax.TaxDetail;
                    checkExistingTax.Coefficient = (double)personalIncomeTax.Coefficient;
                    ctx.SaveChanges();
                    return true;
                }
                else return false;

            }
        }
    }
}