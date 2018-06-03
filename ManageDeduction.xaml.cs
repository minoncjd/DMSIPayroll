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
    /// Interaction logic for ManageDeduction.xaml
    /// </summary>
    public partial class ManageDeduction : MetroWindow
    {
        List<DMSIClass._Deduction> lDeduction = new List<DMSIClass._Deduction>();
        public int empid;
        public ManageDeduction()
        {
            InitializeComponent();
        }

        private void GetDeductions()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    lDeduction = new List<DMSIClass._Deduction>();
                    var deductions = db.Deductions.Where(m => m.EmployeeID == empid).ToList();

                    foreach (var x in deductions)
                    {
                        DMSIClass._Deduction deduction = new DMSIClass._Deduction();
                        var period = db.Periods.Where(m => m.PeriodID == x.PeriodID).FirstOrDefault();
                        var deductiontype = db.DeductionTypes.Where(m => m.DeductionTypeID == x.DeductionTypeID).FirstOrDefault();

                        deduction.DeductionID = x.DeductionID;
                        deduction.DeductionType = deductiontype.Description;
                        deduction.StDate = x.StDate;
                        deduction.ToDate = x.ToDate;
                        deduction.Period = period.PeriodDescription;
                        deduction.Amortization = x.Amortization;

                        lDeduction.Add(deduction);
                    }

                    datagridview.ItemsSource = lDeduction.OrderBy(m => m.DeductionID);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetDeductions();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddDeduction addDeduction = new AddDeduction();
            addDeduction.mode = 1;
            addDeduction.empid = empid;
            addDeduction.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetDeductions();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((DMSIClass._Deduction)datagridview.SelectedItem);
            AddDeduction addDeduction = new AddDeduction();
            addDeduction.deductionid = x.DeductionID;
            addDeduction.mode = 2;
            addDeduction.empid = empid;
            addDeduction.Show();
        }
    }
}
