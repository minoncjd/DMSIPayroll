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
    
    public partial class Overtime
    {
        public int OvertimeID { get; set; }
        public int EmployeID { get; set; }
        public int OvertimeTypeID { get; set; }
        public System.DateTime StDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public System.DateTime PayrollDate { get; set; }
        public decimal Value { get; set; }
        public decimal Amount { get; set; }
        public Nullable<int> PayrollID { get; set; }
    }
}
