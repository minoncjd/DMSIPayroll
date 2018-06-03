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
    /// Interaction logic for ManageOvertime.xaml
    /// </summary>
    public partial class ManageOvertime : MetroWindow
    {
        List<DMSIClass._Overtime> lOvertime = new List<DMSIClass._Overtime>();
        public int empid;
        public ManageOvertime()
        {
            InitializeComponent();
        }

        private void GetOvertime()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    var overtimes = db.Overtimes.ToList();
                    lOvertime = new List<DMSIClass._Overtime>();
                    foreach (var x in overtimes)
                    {
                        DMSIClass._Overtime overtime = new DMSIClass._Overtime();
                        var overtimetype = db.OvertimeTypes.Where(m => m.OvertimeTypeID == x.OvertimeTypeID).FirstOrDefault();
                        var emp = db.Employees.Where(m => m.EmployeeID == x.EmployeID).FirstOrDefault();
                        var position = db.EmployeePositions.Where(m => m.EmployeePositionID == emp.EmployeeID).FirstOrDefault();
                        overtime.OvertimeType = overtimetype.Description;
                        overtime.OvertimeID = x.OvertimeID;
                        overtime.OvertimePeriod = x.StDate.ToShortDateString() + " - " + x.ToDate.ToShortDateString();
                        overtime.Value = x.Value;
                        overtime.PayrollDate = x.PayrollDate;
                        overtime.Amount = x.Amount;
                        overtime.HourlyRate = position.DailyRate / 8;

                        lOvertime.Add(overtime);
                    }

                    datagridview.ItemsSource = lOvertime.OrderByDescending(m => m.OvertimeID);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetOvertime();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddOvertime addOvertime = new AddOvertime();
            addOvertime.mode = 1;
            addOvertime.empid = empid;
            addOvertime.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetOvertime();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((DMSIClass._Overtime)datagridview.SelectedItem);
            AddOvertime addOvertime = new AddOvertime();
            addOvertime.overtimeid = x.OvertimeID;
            addOvertime.mode = 2;
            addOvertime.empid = empid;
            addOvertime.Show();
        }

        private void datagridview_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //if (Convert.ToInt32(((DMSIClass._Overtime)(e.Row.DataContext)).PayrollID) == 0)
            //{
            //    e.Row.Background = new SolidColorBrush(Color.FromRgb(127, 140, 141));
            //}
        }
    }
}
