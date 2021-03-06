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
    
    public partial class Deduction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Deduction()
        {
            this.PayrollDetails_Deduction = new HashSet<PayrollDetails_Deduction>();
        }
    
        public int DeductionID { get; set; }
        public int EmployeeID { get; set; }
        public int PeriodID { get; set; }
        public int DeductionTypeID { get; set; }
        public System.DateTime StDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public decimal Amortization { get; set; }
    
        public virtual DeductionType DeductionType { get; set; }
        public virtual Period Period { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PayrollDetails_Deduction> PayrollDetails_Deduction { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
