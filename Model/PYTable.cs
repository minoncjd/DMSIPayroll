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
    
    public partial class PYTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PYTable()
        {
            this.Payrolls = new HashSet<Payroll>();
        }
    
        public int PYTableID { get; set; }
        public string PYCode { get; set; }
        public System.DateTime StDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public System.DateTime PYDate { get; set; }
        public string Comments { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payroll> Payrolls { get; set; }
    }
}