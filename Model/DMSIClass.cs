using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSIPayroll.Model
{
    public class DMSIClass
    {
        public class Attendance
        {
            public string ID { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            public string Mode { get; set; }
        }

        public class AttendanceSource
        {
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
            public DateTime Date { get; set; }
            public DateTime Time { get; set; }

        }

        public class _Employee
        {
            public int EmployeeID { get; set; }
            public string EmployeeNo { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }
            public string Company { get; set; }
            public int PositionID { get; set; }
            public bool? IsActive { get; set; }
        }

        public class EmployeeComboBox
        {
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
        }
        public class _Income
        {
            public int IncomeID { get; set; }
            public string Name { get; set; }
            public DateTime PayrollDate { get; set; }
            public decimal? Amount { get; set; }
            public decimal? DailyRate { get; set; }
            public int NoOfDays { get; set; }
            public string IncomePeriod { get; set; }
            public int PayrollID { get; set; }
            public int Trip { get; set; }
        }

        public class _Tardy
        {
            public int TardyID { get; set; }
            public string Type { get; set; }
            public decimal? Value { get; set; }
            public decimal? Amount { get; set; }
            public DateTime PayrollDate { get; set; }
            public int PayrollID { get; set; }
        }

        public class _Loan
        {
            public int LoanID{ get; set; }
            public string LoanType { get; set; }
            public DateTime StDate { get; set; }
            public DateTime ToDate { get; set; }
            public string Period { get; set; }
            public decimal? PrincipalAmount { get; set; }
            public decimal? LoanAmount { get; set; }
            public decimal? LoanPayment { get; set; }
            public decimal? Amortization { get; set; }
            public decimal? LoanBalance { get; set; }

        }

        public class _Deduction
        {
            public int DeductionID { get; set; }
            public string DeductionType { get; set; }
            public DateTime StDate { get; set; }
            public DateTime ToDate { get; set; }
            public string Period { get; set; }
            public decimal? Amortization { get; set; }
         
        }

        public class _OtherIncome
        {
            public int OtherIncomeID { get; set; }
            public string IncomeType { get; set; }
            public DateTime StDate { get; set; }
            public DateTime ToDate { get; set; }
            public string Period { get; set; }
            public decimal? Amount { get; set; }

        }


        public class PayrollDetails
        {
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }
            public decimal? BasicRate { get; set; }
            public decimal? BasicNoOfDays { get; set; }
            public decimal? BasicAmount { get; set; }
            public decimal? LateUndertimeNoOfMins { get; set; }
            public decimal? LateUndertimeAmount { get; set; }
            public decimal? Gross { get; set; }
            public decimal? SSS { get; set; }
            public decimal? Philhealth { get; set; }
            public decimal? Pagibig { get; set; }
            public decimal? Net { get; set; }
            public decimal? HolidayNoOfDays { get; set; }
            public decimal? HolidayAmount { get; set; }
            public decimal? Deduction { get; set; }
            public decimal? Loan { get; set; }
            public decimal? NightDiff { get; set; }
            public decimal? OverTime { get; set; }
            public decimal? OtherInc { get; set; }
            public decimal? Adjustment { get; set; }
            public decimal? Leave { get; set; }

            public decimal? RP1NoOfDays { get; set; }
            public decimal? RP1Amount { get; set; }
            public decimal? RP2NoOfTrips { get; set; }
            public decimal? RP2Amount { get; set; }




        }

        public class _Holiday
        {
            public int HolidayID { get; set; }
            public string Name { get; set; }
            public DateTime PayrollDate { get; set; }
            public decimal? Amount { get; set; }
            public decimal? DailyRate { get; set; }
            public decimal? NoOfDays { get; set; }
            public string HolidayPeriod { get; set; }
            public string HolidayType { get; set; }
        }


        public class _Overtime
        {
            public int OvertimeID { get; set; }
            public string Name { get; set; }
            public DateTime PayrollDate { get; set; }
            public decimal? Amount { get; set; }
            public decimal? HourlyRate { get; set; }
            public decimal? Value { get; set; }
            public string OvertimePeriod { get; set; }
            public string OvertimeType { get; set; }
            public int PayrollID { get; set; }
        }

        public class _Adjustment
        {
            public int AdjustmentID { get; set; }
            public string Name { get; set; }
            public DateTime PayrollDate { get; set; }
            public decimal? Amount { get; set; }     
            public int PayrollID { get; set; }
        }



        public class _NightDiff
        {
            public int NightDiffID { get; set; }
            public string Name { get; set; }
            public DateTime PayrollDate { get; set; }
            public decimal? Amount { get; set; }
            public decimal? HourlyRate { get; set; }
            public decimal? Value { get; set; }
            public string NightDiffIDPeriod { get; set; }
            public string NightDiffIDType { get; set; }
            public int PayrollID { get; set; }
        }

        public class CivilStatus
        {
        
            public string Name { get; set; }
            public string Value { get; set; }
        }


    }
}
