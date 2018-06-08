//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Deductions = new HashSet<Deduction>();
            this.Holidays = new HashSet<Holiday>();
            this.Incomes = new HashSet<Income>();
            this.Leaves = new HashSet<Leave>();
            this.Loans = new HashSet<Loan>();
            this.OtherIncomes = new HashSet<OtherIncome>();
            this.Tardies = new HashSet<Tardy>();
            this.Payrolls = new HashSet<Payroll>();
            this.PayrollLogistics = new HashSet<PayrollLogistic>();
        }
    
        public int EmployeeID { get; set; }
        public string EmployeeNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int EmployeePositionID { get; set; }
        public int CompanyID { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> DateHired { get; set; }
        public Nullable<System.DateTime> DateResigned { get; set; }
        public Nullable<int> BiometricsID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> PayrollType { get; set; }
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public string ContactNo { get; set; }
        public string SSS { get; set; }
        public string Philhealth { get; set; }
        public string Tin { get; set; }
        public string HMDF { get; set; }
        public string Guardian { get; set; }
        public string Relationship { get; set; }
        public string GuardianContactNo { get; set; }
    
        public virtual Company Company { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Deduction> Deductions { get; set; }
        public virtual EmployeePosition EmployeePosition { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Holiday> Holidays { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Income> Incomes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Leave> Leaves { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Loan> Loans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OtherIncome> OtherIncomes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tardy> Tardies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payroll> Payrolls { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PayrollLogistic> PayrollLogistics { get; set; }
    }
}
