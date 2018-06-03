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
    /// Interaction logic for AddHoliday.xaml
    /// </summary>
    public partial class AddHoliday : MetroWindow
    {
        public int holidayid;
        public int mode;
        public int empid;
        public AddHoliday()
        {
            InitializeComponent();
        }

        private void GetHoliday()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var holiday = db.Holidays.Where(m => m.HolidayID == holidayid).FirstOrDefault();
                    tbAmount.Text = holiday.Amount.ToString("G29");
                    cbHolidayType.SelectedValue = holiday.HolidayTypeID;
                    dpDate.SelectedDate = holiday.PayrollDate;
                    dpStDate.SelectedDate = holiday.StDate;
                    dpToDate.SelectedDate = holiday.ToDate;
                    tbNoOfDays.Text = holiday.NoOfDays.ToString("G29");
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
                    tbDailyRate.Text = pos.DailyRate.ToString("G29");

                    cbHolidayType.ItemsSource = db.HolidayTypes.OrderBy(m => m.Description).ToList();
                    cbHolidayType.DisplayMemberPath = "Description";
                    cbHolidayType.SelectedValuePath = "HolidayTypeID";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (mode == 2)
            {
                this.Title = "UPDATE HOLIDAY";
                GetHoliday();
            }
        }

        private void TotalAmount()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {

                    var holidatypeid = Convert.ToInt32(cbHolidayType.SelectedValue);
                    var holidaytype = db.HolidayTypes.Where(m => m.HolidayTypeID == holidatypeid).FirstOrDefault();
                    var emp = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();
                    var emppositon = db.EmployeePositions.Where(m => m.EmployeePositionID == emp.EmployeePositionID).FirstOrDefault();
                    var noofdays = Convert.ToInt32(tbNoOfDays.Text);

                   
                    var hourlyrate = (emppositon.DailyRate) / 8;
                    var holidaypay = (hourlyrate * holidaytype.Multiplier * 8) * noofdays;
                    tbAmount.Text = holidaypay.ToString("G29");

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
                    if (cbHolidayType.SelectedItem == null || tbNoOfDays.Text == "" || tbAmount.Text == "" || dpDate.SelectedDate == null || dpStDate.SelectedDate == null || dpToDate.SelectedDate == null)
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }


                    if (mode == 1)
                    {
                        Holiday holiday = new Holiday();
                        holiday.EmployeeID = empid;
                        holiday.PayrollDate = dpDate.SelectedDate.Value;
                        holiday.StDate = dpStDate.SelectedDate.Value;
                        holiday.ToDate = dpToDate.SelectedDate.Value;
                        holiday.Amount = Decimal.Parse(tbAmount.Text);
                        holiday.HolidayTypeID = Convert.ToInt32(cbHolidayType.SelectedValue);
                        holiday.NoOfDays = Decimal.Parse(tbNoOfDays.Text);
                        db.Holidays.Add(holiday);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var holiday = db.Holidays.Where(m => m.HolidayID == holidayid).FirstOrDefault();
                        holiday.EmployeeID = empid;
                        holiday.PayrollDate = dpDate.SelectedDate.Value;
                        holiday.StDate = dpStDate.SelectedDate.Value;
                        holiday.ToDate = dpToDate.SelectedDate.Value;
                        holiday.Amount = Decimal.Parse(tbAmount.Text);
                        holiday.HolidayTypeID = Convert.ToInt32(cbHolidayType.SelectedValue);
                        holiday.NoOfDays = Decimal.Parse(tbNoOfDays.Text);
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

        private void tbNoOfDays_LostFocus(object sender, RoutedEventArgs e)
        {
            TotalAmount();
        }

        private void clear()
        {
            tbNoOfDays.Text = "";
            tbAmount.Text = "";
            cbHolidayType.Text = "";
            dpDate.SelectedDate = null;
            dpToDate.SelectedDate = null;
            dpStDate.SelectedDate = null;

        }
    }


}
