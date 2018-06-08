using DMSIPayroll.Model;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DMSIPayroll
{
    /// <summary>
    /// Interaction logic for PayrollListLogistics.xaml
    /// </summary>
    public partial class PayrollListLogistics : MetroWindow
    {
        List<DMSIClass.PayrollList> lPayrollList = new List<DMSIClass.PayrollList>();
        public int pytableid;

        public PayrollListLogistics()
        {
            InitializeComponent();
        }

        private void GetPayrolls()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    lPayrollList = new List<DMSIClass.PayrollList>();
                    var payrolls = db.PayrollLogistics.Where(m => m.PYTableID == pytableid).ToList();
                    foreach (var x in payrolls)
                    {
                        DMSIClass.PayrollList payroll = new DMSIClass.PayrollList();
                        var employee = db.Employees.Where(m => m.EmployeeID == x.EmployeeID).FirstOrDefault();
                        var position = db.EmployeePositions.Where(m => m.EmployeePositionID == employee.EmployeePositionID).FirstOrDefault();
                        payroll.Position = position.PositionName;
                        payroll.EmployeeName = employee.LastName + ", " + employee.FirstName;
                        payroll.EmployeeNumber = employee.EmployeeNumber;
                        payroll.RP1 = x.RP1;
                        payroll.RP2 = x.RP2;
                        payroll.ND = x.NightDifferential;
                        payroll.OT = x.Overtime;
                        payroll.HO = x.Holiday;
                        payroll.Late = x.Tardiness;
                        payroll.Adjustment = x.Adjustment;
                        payroll.OtherInc = x.OtherIncome;
                        payroll.Gross = x.Gross;
                        payroll.Pagibig = x.PAGIBIG;
                        payroll.Sss = x.SSS;
                        payroll.Philhealth = x.PHILHEALTH;
                        payroll.Loan = x.Loan;
                        payroll.Deduction = x.Deduction;
                        payroll.Leave = x.Leaves;
                        payroll.Net = x.Net;
                        payroll.PayrollID = x.PayrollID;

                        lPayrollList.Add(payroll);
                    }

                    datagridview.ItemsSource = lPayrollList.OrderBy(m => m.EmployeeName).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetPayrolls();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetPayrolls();
        }
    }
}
