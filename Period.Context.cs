﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Api_Salary_Management
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SalaryManagement_SWD391_ProjectEntities_Period : DbContext
    {
        public SalaryManagement_SWD391_ProjectEntities_Period()
            : base("name=SalaryManagement_SWD391_ProjectEntities_Period")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Period> Periods { get; set; }
    }
}