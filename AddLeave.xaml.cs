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
    /// Interaction logic for AddLeave.xaml
    /// </summary>
    public partial class AddLeave : MetroWindow
    {
        public int mode;
        public int leaveid;
        public int empid;

        public AddLeave()
        {
            InitializeComponent();
        }

        private void GetLeave()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var leave = db.Leaves.Where(m => m.LeaveID == leaveid).FirstOrDefault();
                    tbAmount.Text = leave.Amount.ToString("G29");
                    dpDate.SelectedDate = leave.PayrollDate;
                    tbValue.Text = leave.Value.ToString();
                    
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    if (tbAmount.Text == "" || dpDate.SelectedDate == null)
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (mode == 1)
                    {
                        Leave leave = new Leave();
                        leave.EmployeeID = empid;
                        leave.PayrollDate = dpDate.SelectedDate.Value;
                        leave.Amount = Decimal.Parse(tbAmount.Text);
                        leave.Value = Decimal.Parse(tbValue.Text);
                        db.Leaves.Add(leave);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var leave = db.Leaves.Where(m => m.LeaveID == leaveid).FirstOrDefault();
                        leave.EmployeeID = empid;
                        leave.PayrollDate = dpDate.SelectedDate.Value;
                        leave.Amount = Decimal.Parse(tbAmount.Text);
                        leave.Value = Decimal.Parse(tbValue.Text);
                        db.SaveChanges();
                        MessageBox.Show("Update Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);

                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void clear()
        {
            tbAmount.Text = "";          
            dpDate.SelectedDate = null;
            tbValue.Text = "";
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {

                    var emp = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();
                    var pos = db.EmployeePositions.Where(m => m.EmployeePositionID == emp.EmployeePositionID).FirstOrDefault();

                    tbEmployeeName.Text = (emp.LastName + ", " + emp.FirstName + " " + emp.MiddleName).ToUpper();
                    tbDailyRate.Text = pos.DailyRate.ToString("G29");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (mode == 2)
            {
                this.Title = "UPDATE LEAVE";
                GetLeave();
            }
        }

        private void TotalAmount()
        {
            double dailyrate = Convert.ToDouble(tbDailyRate.Text);
            double value = Convert.ToDouble(tbValue.Text);
            tbAmount.Text = (dailyrate * value).ToString();
        }

        private void tbValue_LostFocus(object sender, RoutedEventArgs e)
        {
            TotalAmount();
        }
    }
}
