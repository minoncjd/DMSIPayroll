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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : MetroWindow
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            ManageEmployee manageEmployee = new ManageEmployee();
            manageEmployee.Show();
        }

        private void btnPayrollDetails_Click(object sender, RoutedEventArgs e)
        {
            EmployeeMaster employeeMaster = new EmployeeMaster();
            employeeMaster.Show();
        }

        private void btnUtilities_Click(object sender, RoutedEventArgs e)
        {


        }

        private void btnCompany_Click(object sender, RoutedEventArgs e)
        {
            ManageCompany manageCompany = new ManageCompany();
            manageCompany.Show();
        }

        private void btnPositions_Click(object sender, RoutedEventArgs e)
        {
            ManagePosition managePosition = new ManagePosition();
            managePosition.Show();
        }

        private void btnLoanTypes_Click(object sender, RoutedEventArgs e)
        {
            ManageLoanType manageLoanType = new ManageLoanType();
            manageLoanType.Show();
        }

        private void btnIncomeTypes_Click(object sender, RoutedEventArgs e)
        {
            ManageIncomeType manageIncomeType = new ManageIncomeType();
            manageIncomeType.Show();
        }

        private void btnAdjustmentTypes_Click(object sender, RoutedEventArgs e)
        {
           

        }

        private void btnPeriods_Click(object sender, RoutedEventArgs e)
        {
            ManagePeriod managePeriod = new ManagePeriod();
            managePeriod.Show();
        }

        private void btnAttendance_Click(object sender, RoutedEventArgs e)
        {
            ReadAttendance readAttendance = new ReadAttendance();
            readAttendance.Show();    
        }

        private void btnProcessPayroll_Click(object sender, RoutedEventArgs e)
        {
            PayrollProcessMenu payrollProcessMenu = new PayrollProcessMenu();
            payrollProcessMenu.Show();
        }

        private void btnReportMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHolidayTypes_Click(object sender, RoutedEventArgs e)
        {
            ManageHolidayType manageHolidayType = new ManageHolidayType();
            manageHolidayType.Show();
        }

        private void btnHoliday_Click(object sender, RoutedEventArgs e)
        {
            ManageHoliday manageHoliday = new ManageHoliday();
            manageHoliday.Show();
        }

        private void btnDeductionType_Click(object sender, RoutedEventArgs e)
        {
            ManageDeductionType manageDeductionType = new ManageDeductionType();
            manageDeductionType.Show();
        }

        private void btnBulkIncome_Click(object sender, RoutedEventArgs e)
        {
            BulkAddIncome bulkAddIncome = new BulkAddIncome();
            bulkAddIncome.Show();
        }

        private void btnEmployeeDTR_Click(object sender, RoutedEventArgs e)
        {
            PrintDTR printDTR = new PrintDTR();
            printDTR.Show();
        }

        private void btnOvertimeType_Click(object sender, RoutedEventArgs e)
        {
            ManageOvertimeType manageOvertimeType = new ManageOvertimeType();
            manageOvertimeType.Show();
        }

        private void btnNightShiftTypes_Click(object sender, RoutedEventArgs e)
        {
            ManageNightShiftType manageNightShiftType = new ManageNightShiftType();
            manageNightShiftType.Show();
        }
    }
}
