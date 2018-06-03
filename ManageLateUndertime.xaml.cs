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
    public partial class ManageLateUndertime : MetroWindow
    {
        List<DMSIClass._Tardy> lTardy = new List<DMSIClass._Tardy>();
        public int empid;
        public ManageLateUndertime()
        {
            InitializeComponent();
        }

       private void GetAdjustments()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    lTardy = new List<DMSIClass._Tardy>();
                    var tardies = db.Tardies.Where(m=>m.EmployeeID == empid).ToList();

                    foreach (var x in tardies)
                    {
                        DMSIClass._Tardy tardy = new DMSIClass._Tardy();
                    


                        tardy.Amount = x.Amount;
                        tardy.PayrollDate = x.PayrollDate;
                        tardy.Value = x.Value;
                        tardy.TardyID = x.TardyID;
                        if (x.Type == 1)
                        {
                            tardy.Type = "Late";
                        }
                        else
                        {
                            tardy.Type = "Undertime";
                        }



                        lTardy.Add(tardy);
                    }

                    datagridview.ItemsSource = lTardy.OrderByDescending(m => m.TardyID);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetAdjustments();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddLateUndertime addLateUndertime = new AddLateUndertime();
            addLateUndertime.mode = 1;
            addLateUndertime.empid = empid;
            addLateUndertime.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetAdjustments();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((DMSIClass._Tardy)datagridview.SelectedItem);
            AddLateUndertime addLateUndertime = new AddLateUndertime();
            addLateUndertime.tardyid = x.TardyID;
            addLateUndertime.empid = empid;
            addLateUndertime.mode = 2;
            addLateUndertime.Show();
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
