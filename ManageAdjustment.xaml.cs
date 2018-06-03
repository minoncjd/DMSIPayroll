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
    /// Interaction logic for ManageAdjustment.xaml
    /// </summary>
    public partial class ManageAdjustment : MetroWindow
    {
        List<DMSIClass._Adjustment> lAdjustment = new List<DMSIClass._Adjustment>();
        public int empid;

        public ManageAdjustment()
        {
            InitializeComponent();
        }

        private void GetAdjustment()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    var adjustments = db.Adjustments.ToList();
                    lAdjustment = new List<DMSIClass._Adjustment>();
                    foreach (var x in adjustments)
                    {
                        DMSIClass._Adjustment adjustment = new DMSIClass._Adjustment();
                        var emp = db.Employees.Where(m => m.EmployeeID == x.EmployeeID).FirstOrDefault();
                        var position = db.EmployeePositions.Where(m => m.EmployeePositionID == emp.EmployeeID).FirstOrDefault();

                        adjustment.AdjustmentID = x.AdjustmentID;
                        adjustment.PayrollDate = x.PayrollDate;
                        adjustment.Amount = x.Amount;
                        lAdjustment.Add(adjustment);
                    }

                    datagridview.ItemsSource = lAdjustment.OrderByDescending(m => m.AdjustmentID);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetAdjustment();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddAdjustment addAdjustment = new AddAdjustment();
            addAdjustment.mode = 1;
            addAdjustment.empid = empid;
            addAdjustment.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetAdjustment();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((DMSIClass._Adjustment)datagridview.SelectedItem);
            AddAdjustment addAdjustment = new AddAdjustment();
            addAdjustment.adjustmentid = x.AdjustmentID;
            addAdjustment.mode = 2;
            addAdjustment.empid = empid;
            addAdjustment.Show();
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
