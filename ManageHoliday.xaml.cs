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
    /// Interaction logic for ManageHoliday.xaml
    /// </summary>
    public partial class ManageHoliday : MetroWindow
    {
        List<DMSIClass._Holiday> lHoliday = new List<DMSIClass._Holiday>();
        public int empid;
        public ManageHoliday()
        {
            InitializeComponent();
        }

        private void GetHoliday()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    var holidays = db.Holidays.Where(m=>m.EmployeeID == empid).ToList();
                    lHoliday = new List<DMSIClass._Holiday>();
                    foreach (var x in holidays)
                    {
                        DMSIClass._Holiday holiday = new DMSIClass._Holiday();
                        var holidaytype = db.HolidayTypes.Where(m => m.HolidayTypeID == x.HolidayTypeID).FirstOrDefault();
                        var emp = db.Employees.Where(m => m.EmployeeID == x.EmployeeID).FirstOrDefault();
                        var position = db.EmployeePositions.Where(m => m.EmployeePositionID == emp.EmployeePositionID).FirstOrDefault();
                        holiday.HolidayType = holidaytype.Description;
                        holiday.HolidayID = x.HolidayID;
                        holiday.HolidayPeriod = x.StDate.ToShortDateString() + " - " + x.ToDate.ToShortDateString();
                        holiday.NoOfDays = x.NoOfDays;
                        holiday.PayrollDate = x.PayrollDate;
                        holiday.Amount = x.Amount;
                        holiday.DailyRate = position.DailyRate;
                 
                        lHoliday.Add(holiday);
                    }

                    datagridview.ItemsSource = lHoliday.OrderByDescending(m=>m.HolidayID);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetHoliday();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddHoliday addHoliday = new AddHoliday();
            addHoliday.mode = 1;
            addHoliday.empid = empid;
            addHoliday.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetHoliday();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((DMSIClass._Holiday)datagridview.SelectedItem);
            AddHoliday addHoliday = new AddHoliday();
            addHoliday.holidayid = x.HolidayID;
            addHoliday.mode = 2;
            addHoliday.empid = empid;
            addHoliday.Show();
        }

        private void datagridview_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //if (Convert.ToInt32(((DMSIClass._Adjustment)(e.Row.DataContext)).PayrollID) == 0)
            //{
            //    e.Row.Background = new SolidColorBrush(Color.FromRgb(127, 140, 141));
            //}
        }
    }
}
