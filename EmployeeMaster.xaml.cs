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
    /// Interaction logic for EmployeeMaster.xaml
    /// </summary>
    public partial class EmployeeMaster : MetroWindow
    {

        List<TabItem> lTabItem = new List<TabItem>();
        public int empid;
        Employee employee = new Employee();
        public EmployeeMaster()
        {
            InitializeComponent();
        }


        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {

                    cbEmployee.SelectedValue = empid;

                    var emp = db.Employees.ToList();
                    List<DMSIClass.EmployeeComboBox> lEmployee = new List<DMSIClass.EmployeeComboBox>();

                    foreach (var x in emp)
                    {
                        DMSIClass.EmployeeComboBox employee = new DMSIClass.EmployeeComboBox();
                        employee.EmployeeID = x.EmployeeID;
                        employee.EmployeeName = (x.LastName + ", " + x.FirstName + " " + x.MiddleName).ToUpper();
                        lEmployee.Add(employee);
                    }

                    cbEmployee.ItemsSource = lEmployee.OrderBy(m => m.EmployeeName);
                    cbEmployee.DisplayMemberPath = "EmployeeName";
                    cbEmployee.SelectedValuePath = "EmployeeID";

                    if (empid != 0)
                    {
                        GetEmployeeDetails(empid);
                    }
                    else
                    {
                        DisableButton();
                    }
                    

                    
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void GetEmployeeDetails(int empid)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    employee = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();
                    var company = db.Companies.Where(m => m.CompanyID == employee.CompanyID).FirstOrDefault();
                    var position = db.EmployeePositions.Where(m => m.EmployeePositionID == employee.EmployeePositionID).FirstOrDefault();

                    tbCompany.Text = company.CompanyName;
                    tbDailyRate.Text = position.DailyRate.ToString();
                    tbEmployeeNo.Text = employee.EmployeeNumber;
                    tbName.Text = (employee.LastName + ", " + employee.FirstName + " " + employee.MiddleName).ToUpper();
                    tbPosition.Text = position.PositionName;

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        
        private void cbEmployee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var empid = Convert.ToInt32(cbEmployee.SelectedValue);
                GetEmployeeDetails(empid);
                EnableButton();
            }
        }

        private void btnViewIncome_Click(object sender, RoutedEventArgs e)
        {
            var empid = Convert.ToInt32(cbEmployee.SelectedValue);

            if (employee.PayrollType == 1)
            {
                ManageIncomeLogistics manageIncomeLogistics = new ManageIncomeLogistics();
                manageIncomeLogistics.empid = empid;
                manageIncomeLogistics.Show();
            }
            else
            {
                ManageIncome manageIncome = new ManageIncome();
                manageIncome.empid = empid;
                manageIncome.Show();
            }
        
        }

        private void btnViewAdjustment_Click(object sender, RoutedEventArgs e)
        {
            var empid = Convert.ToInt32(cbEmployee.SelectedValue);
            ManageLateUndertime manageLateUndertime = new ManageLateUndertime();
            manageLateUndertime.empid = empid;
            manageLateUndertime.Show();
        }

        private void btnViewLoan_Click(object sender, RoutedEventArgs e)
        {
            var empid = Convert.ToInt32(cbEmployee.SelectedValue);
            ManageLoan manageLoan = new ManageLoan();
            manageLoan.empid = empid;
            manageLoan.Show();
        }

        private void EnableButton()
        {
            btnViewAdjustment.IsEnabled = true;
            btnViewIncome.IsEnabled = true;
            btnViewDeduction.IsEnabled = true;
            btnViewLoan.IsEnabled = true;
            btnViewHoliday.IsEnabled = true;
            btnOverTime.IsEnabled = true;
            btnNightDiff.IsEnabled = true;
            btnAdjustment.IsEnabled = true;
        }

        private void DisableButton()
        {
            btnViewAdjustment.IsEnabled = false;
            btnViewIncome.IsEnabled = false;
            btnViewDeduction.IsEnabled = false;
            btnViewLoan.IsEnabled = false;
            btnViewHoliday.IsEnabled = false;
            btnOverTime.IsEnabled = false;
            btnNightDiff.IsEnabled = false;
            btnAdjustment.IsEnabled = false;
        }

        private void btnViewHoliday_Click(object sender, RoutedEventArgs e)
        {
            var empid = Convert.ToInt32(cbEmployee.SelectedValue);
            ManageHoliday manageHoliday = new ManageHoliday();
            manageHoliday.empid = empid;
            manageHoliday.Show();
        }

     

        private void btnViewDeduction_Click(object sender, RoutedEventArgs e)
        {
            var empid = Convert.ToInt32(cbEmployee.SelectedValue);
            ManageDeduction manageDeduction = new ManageDeduction();
            manageDeduction.empid = empid;
            manageDeduction.Show();
        }

        private void btnOverTime_Click(object sender, RoutedEventArgs e)
        {
            var empid = Convert.ToInt32(cbEmployee.SelectedValue);
            ManageOvertime manageOvertime = new ManageOvertime();
            manageOvertime.empid = empid;
            manageOvertime.Show();
        }

        private void btnNightDiff_Click(object sender, RoutedEventArgs e)
        {
            var empid = Convert.ToInt32(cbEmployee.SelectedValue);
            ManageNightDifferential manageNightDifferential = new ManageNightDifferential();
            manageNightDifferential.empid = empid;
            manageNightDifferential.Show();
        }

        private void btnAdjustment_Click(object sender, RoutedEventArgs e)
        {
            var empid = Convert.ToInt32(cbEmployee.SelectedValue);
            ManageAdjustment manageAdjustment = new ManageAdjustment();
            manageAdjustment.empid = empid;
            manageAdjustment.Show();
        }

        private void btnViewOtherIncome_Click(object sender, RoutedEventArgs e)
        {
            var empid = Convert.ToInt32(cbEmployee.SelectedValue);
            ManageOtherIncome manageOtherIncome = new ManageOtherIncome();
            manageOtherIncome.empid = empid;
            manageOtherIncome.Show();
        }

        private void btnViewLeave_Click(object sender, RoutedEventArgs e)
        {
            var empid = Convert.ToInt32(cbEmployee.SelectedValue);
            ManageLeave manageLeave = new ManageLeave();
            manageLeave.empid = empid;
            manageLeave.Show();
        }
    }
}
