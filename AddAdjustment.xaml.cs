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
    public partial class AddAdjustment : MetroWindow
    {

        public int mode;
        public int adjustmentid;
        public int empid;

        public AddAdjustment()
        {
            InitializeComponent();
        }

        private void GetAdjustment()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var adjustment = db.Adjustments.Where(m => m.AdjustmentID == adjustmentid).FirstOrDefault();
                    tbAmount.Text = adjustment.Amount.ToString("G29");
                    dpDate.SelectedDate = adjustment.PayrollDate;                  
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
                    tbEmployeeName.Text = (emp.LastName + ", " + emp.FirstName + " " + emp.MiddleName).ToUpper();       
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (mode == 2)
            {
                this.Title = "UPDATE ADJUSTMENT";
                GetAdjustment();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    if (tbAmount.Text == "" || dpDate.SelectedDate == null )
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (mode == 1)
                    {
                        Adjustment adjustment = new Adjustment();
                        adjustment.EmployeeID = empid;
                        adjustment.PayrollDate = dpDate.SelectedDate.Value;
                        adjustment.Amount = Decimal.Parse(tbAmount.Text);
                       
                        db.Adjustments.Add(adjustment);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var adjustment = db.Adjustments.Where(m => m.AdjustmentID == adjustmentid).FirstOrDefault();
                        adjustment.EmployeeID = empid;
                        adjustment.PayrollDate = dpDate.SelectedDate.Value;
                        adjustment.Amount = Decimal.Parse(tbAmount.Text);
                        
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
        }
    }
}
