﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DMSIPayroll.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DMSIPayrollEntities : DbContext
    {
        public DMSIPayrollEntities()
            : base("name=DMSIPayrollEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<EmployeePosition> EmployeePositions { get; set; }
        public virtual DbSet<IncomeType> IncomeTypes { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }
        public virtual DbSet<LoanType> LoanTypes { get; set; }
        public virtual DbSet<Period> Periods { get; set; }
        public virtual DbSet<HolidayType> HolidayTypes { get; set; }
        public virtual DbSet<DeductionType> DeductionTypes { get; set; }
        public virtual DbSet<Deduction> Deductions { get; set; }
        public virtual DbSet<PYSSS> PYSSSes { get; set; }
        public virtual DbSet<Philhealth> Philhealths { get; set; }
        public virtual DbSet<PYTable> PYTables { get; set; }
        public virtual DbSet<PayrollDetails_Deduction> PayrollDetails_Deduction { get; set; }
        public virtual DbSet<PayrollDetails_Loan> PayrollDetails_Loan { get; set; }
        public virtual DbSet<BiometricsLog> BiometricsLogs { get; set; }
        public virtual DbSet<Tardy> Tardies { get; set; }
        public virtual DbSet<OvertimeType> OvertimeTypes { get; set; }
        public virtual DbSet<Overtime> Overtimes { get; set; }
        public virtual DbSet<NightDifferential> NightDifferentials { get; set; }
        public virtual DbSet<NightShiftType> NightShiftTypes { get; set; }
        public virtual DbSet<Adjustment> Adjustments { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<OtherIncome> OtherIncomes { get; set; }
        public virtual DbSet<Leave> Leaves { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<PayrollDetails_OtherIncome> PayrollDetails_OtherIncome { get; set; }
        public virtual DbSet<Payroll> Payrolls { get; set; }
        public virtual DbSet<PayrollLogistic> PayrollLogistics { get; set; }
    
        public virtual ObjectResult<GetEmployeeDTR_Result> GetEmployeeDTR(string startDate, string endDate, Nullable<int> bioid)
        {
            var startDateParameter = startDate != null ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(string));
    
            var bioidParameter = bioid.HasValue ?
                new ObjectParameter("bioid", bioid) :
                new ObjectParameter("bioid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetEmployeeDTR_Result>("GetEmployeeDTR", startDateParameter, endDateParameter, bioidParameter);
        }
    }
}
