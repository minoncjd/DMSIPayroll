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
    /// Interaction logic for AddOvertime.xaml
    /// </summary>
    public partial class AddOvertime : MetroWindow
    {
        public int mode;
        public int overtimeid;
        public int empid;
        public AddOvertime()
        {
            InitializeComponent();
        }

        private void GetHoliday()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var overtime = db.Overtimes.Where(m => m.OvertimeID == overtimeid).FirstOrDefault();
                    tbAmount.Text = overtime.Amount.ToString("G29");
                    cbOvertimeType.SelectedValue = overtime.OvertimeTypeID;
                    dpDate.SelectedDate = overtime.PayrollDate;
                    dpStDate.SelectedDate = overtime.StDate;
                    dpToDate.SelectedDate = overtime.ToDate;
                    tbValue.Text = overtime.Value.ToString("G29");
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

                    tbEmployeeName.Text = (emp.LastName + ", " + emp.FirstName + " " + emp.MiddleName).ToUpper();
                    tbHourlyRate.Text = (pos.DailyRate / 8).ToString("G29");

                    cbOvertimeType.ItemsSource = db.OvertimeTypes.OrderBy(m => m.Description).ToList();
                    cbOvertimeType.DisplayMemberPath = "Description";
                    cbOvertimeType.SelectedValuePath = "OvertimeTypeID";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (mode == 2)
            {
                this.Title = "UPDATE OVERTIME";
                GetHoliday();
            }
        }

     

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    if (tbValue.Text == "" || tbAmount.Text == "" || cbOvertimeType.SelectedItem == null || dpDate.SelectedDate == null || dpStDate.SelectedDate == null || dpToDate.SelectedDate == null)
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (mode == 1)
                    {
                        Overtime overtime = new Overtime();
                        overtime.EmployeID = empid;
                        overtime.PayrollDate = dpDate.SelectedDate.Value;
                        overtime.StDate = dpStDate.SelectedDate.Value;
                        overtime.ToDate = dpToDate.SelectedDate.Value;
                        overtime.Amount = Decimal.Parse(tbAmount.Text);
                        overtime.OvertimeTypeID = Convert.ToInt32(cbOvertimeType.SelectedValue);
                        overtime.Value = Convert.ToInt32(tbValue.Text);
                        db.Overtimes.Add(overtime);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var overtime = db.Overtimes.Where(m => m.OvertimeID == overtimeid).FirstOrDefault();
                        overtime.EmployeID = empid;
                        overtime.PayrollDate = dpDate.SelectedDate.Value;
                        overtime.StDate = dpStDate.SelectedDate.Value;
                        overtime.ToDate = dpToDate.SelectedDate.Value;
                        overtime.Amount = Decimal.Parse(tbAmount.Text);
                        overtime.OvertimeTypeID = Convert.ToInt32(cbOvertimeType.SelectedValue);
                        overtime.Value = Decimal.Parse(tbValue.Text);
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

                    var overtimetypeid = Convert.ToInt32(cbOvertimeType.SelectedValue);
                    var overtimetype = db.OvertimeTypes.Where(m => m.OvertimeTypeID == overtimetypeid).FirstOrDefault();
                    var emp = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();
                    var emppositon = db.EmployeePositions.Where(m => m.EmployeePositionID == emp.EmployeePositionID).FirstOrDefault();
                    var noofhours = Convert.ToInt32(tbValue.Text);


                    var hourlyrate = (emppositon.DailyRate) / 8;
                    var overtimepay = (hourlyrate * overtimetype.Multiplier) * noofhours;
                    tbAmount.Text = overtimepay.ToString();

                }

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void tbNoOfDays_LostFocus(object sender, RoutedEventArgs e)
        {
            TotalAmount();
        }

        private void clear()
        {
            tbAmount.Text = "";
            tbValue.Text = "";
            cbOvertimeType.Text = "";
            dpDate.SelectedDate = null;
            dpToDate.SelectedDate = null;
            dpStDate.SelectedDate = null;


        }
    }
}
