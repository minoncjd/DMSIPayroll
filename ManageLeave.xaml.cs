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
    /// Interaction logic for ManageLeave.xaml
    /// </summary>
    public partial class ManageLeave : MetroWindow
    {
        List<Leave> lLeave = new List<Leave>();
        public int empid;

        public ManageLeave()
        {
            InitializeComponent();
        }

        private void GetAdjustment()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    var leaves = db.Leaves.Where(m=>m.EmployeeID == empid).ToList();
                    lLeave = new List<Leave>();
                    foreach (var x in leaves)
                    {
                        Leave leave = new Leave();
          
                        leave.LeaveID = x.LeaveID;
                        leave.PayrollDate = x.PayrollDate;
                        leave.Amount = x.Amount;
                        leave.Value = x.Value;
                       
                        lLeave.Add(leave);
                    }

                    datagridview.ItemsSource = lLeave.OrderByDescending(m => m.LeaveID);
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
            AddLeave addLeave = new AddLeave();
            addLeave.mode = 1;
            addLeave.empid = empid;
            addLeave.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetAdjustment();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((Leave)datagridview.SelectedItem);
            AddLeave addLeave = new AddLeave();
            addLeave.leaveid = x.LeaveID;
            addLeave.mode = 2;
            addLeave.empid = empid;
            addLeave.Show();
        }
    }
}
