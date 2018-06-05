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
    /// Interaction logic for ManageNightDifferential.xaml
    /// </summary>
    public partial class ManageNightDifferential : MetroWindow
    {
        List<DMSIClass._NightDiff> lNightDiff = new List<DMSIClass._NightDiff>();
        public int empid;
        public ManageNightDifferential()
        {
            InitializeComponent();
        }

        private void GetHoliday()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    var nightdiffs = db.NightDifferentials.ToList();
                    lNightDiff = new List<DMSIClass._NightDiff>();
                    foreach (var x in nightdiffs)
                    {
                        DMSIClass._NightDiff nightdiff = new DMSIClass._NightDiff();
                        var nightshifttype = db.NightShiftTypes.Where(m => m.NightShiftTypeID == x.NightShiftTypeID).FirstOrDefault();
                        var emp = db.Employees.Where(m => m.EmployeeID == x.EmployeeID).FirstOrDefault();
                        var position = db.EmployeePositions.Where(m => m.EmployeePositionID == emp.EmployeePositionID).FirstOrDefault();
                        nightdiff.NightDiffIDType = nightshifttype.Description;
                        nightdiff.NightDiffID = x.NightDifferentialID;
                        nightdiff.NightDiffIDPeriod = x.StDate.ToShortDateString() + " - " + x.ToDate.ToShortDateString();
                        nightdiff.Value = x.Value;
                        nightdiff.PayrollDate = x.PayrollDate;
                        nightdiff.Amount = x.Amount;
                        nightdiff.HourlyRate = position.DailyRate / 8;

                        lNightDiff.Add(nightdiff);
                    }

                    datagridview.ItemsSource = lNightDiff.OrderByDescending(m => m.NightDiffID);
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
            AddNightDiffrential addNightDiffrential = new AddNightDiffrential();
            addNightDiffrential.mode = 1;
            addNightDiffrential.empid = empid;
            addNightDiffrential.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetHoliday();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((DMSIClass._NightDiff)datagridview.SelectedItem);
            AddNightDiffrential addNightDiffrential = new AddNightDiffrential();
            addNightDiffrential.nightdiffid = x.NightDiffID;
            addNightDiffrential.mode = 2;
            addNightDiffrential.empid = empid;
            addNightDiffrential.Show();
        }

        private void datagridview_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //if (Convert.ToInt32(((DMSIClass._NightDiff)(e.Row.DataContext)).PayrollID) == 0)
            //{
            //    e.Row.Background = new SolidColorBrush(Color.FromRgb(127, 140, 141));
            //}
        }
    }
}
