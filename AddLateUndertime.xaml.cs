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
    /// Interaction logic for AddAdjustment.xaml
    /// </summary>
    public partial class AddLateUndertime : MetroWindow
    {
        public int empid;
        public int mode;
        public int tardyid;

        public AddLateUndertime()
        {
            InitializeComponent();
        }


        private void GetTardy()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var tardy = db.Tardies.Where(m => m.TardyID == tardyid).FirstOrDefault();
                    tbAmount.Text = tardy.Amount.ToString("G29");
                    tbValue.Text = tardy.Value.ToString("G29");
                    dpPayrollDate.SelectedDate = tardy.PayrollDate;

                    if (tardy.Type == 1)
                    {
                        rbLate.IsChecked = true;
                    }
                    else
                    {
                        rbUndertime.IsChecked = true;
                    }
                }
            }

            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {

                    var emp = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();
                    var pos = db.EmployeePositions.Where(m => m.EmployeePositionID == emp.EmployeePositionID).FirstOrDefault();

                    tbEmployee.Text = (emp.LastName + ", " + emp.FirstName + " " + emp.MiddleName).ToUpper();
                    tbHourlyRate.Text = (pos.DailyRate / 8).ToString("G29");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            if (mode == 2)
            {
                this.Title = "UPDATE LATE UNDERTIME";
                GetTardy();

            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {

                    if (tbAmount.Text == "" || tbValue.Text == "" || dpPayrollDate.SelectedDate == null || rbLate.IsChecked == false && rbUndertime.IsChecked == false)
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;

                    }
                    if (mode == 1)
                    {
                        Tardy tardy = new Tardy();
                        tardy.Amount = Decimal.Parse(tbAmount.Text);
                        tardy.EmployeeID = empid;
                        tardy.PayrollDate = dpPayrollDate.SelectedDate.Value;
                        tardy.Value = Convert.ToInt32(tbValue.Text);
                        if (rbLate.IsChecked == true)
                        {
                            tardy.Type = 1;
                        }
                        else
                        {
                            tardy.Type = 2;
                        }

                        db.Tardies.Add(tardy);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var tardy = db.Tardies.Where(m => m.TardyID == tardyid).FirstOrDefault();
                        tardy.Amount = Decimal.Parse(tbAmount.Text);
                        tardy.EmployeeID = empid;
                        tardy.PayrollDate = dpPayrollDate.SelectedDate.Value;
                        tardy.Value = Convert.ToInt32(tbValue.Text);
                        if (rbLate.IsChecked == true)
                        {
                            tardy.Type = 1;
                        }
                        else
                        {
                            tardy.Type = 2;
                        }
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

        private void TotalAmount()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {

                    var emp = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();
                    var emppositon = db.EmployeePositions.Where(m => m.EmployeePositionID == emp.EmployeePositionID).FirstOrDefault();
                    var value = Convert.ToInt32(tbValue.Text);

                    var hourlyrate = (emppositon.DailyRate) / 8;
                    var late = hourlyrate / 60 * value;
                    tbAmount.Text = late.ToString();

                }

            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void tbValue_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbValue.Text))
            {
                return;
            }
            else
            {
                TotalAmount();
            }
        }

        private void clear()
        {
            tbValue.Text = "";
            tbAmount.Text = "";
            dpPayrollDate.SelectedDate = null;
            rbLate.IsChecked = false;
            rbUndertime.IsChecked = false;
        }
    }
}
