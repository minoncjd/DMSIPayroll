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
    /// Interaction logic for ManageOtherIncome.xaml
    /// </summary>
    public partial class ManageOtherIncome : MetroWindow
    {
        List<DMSIClass._OtherIncome> lOtherIncome = new List<DMSIClass._OtherIncome>();

        public int empid;
        public ManageOtherIncome()
        {
            InitializeComponent();
        }

        private void GetOtherIncomes()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    lOtherIncome = new List<DMSIClass._OtherIncome>();
                    var otherincomes = db.OtherIncomes.Where(m => m.EmployeeID == empid).ToList();

                    foreach (var x in otherincomes)
                    {
                        DMSIClass._OtherIncome otherIncome = new DMSIClass._OtherIncome();
                        var period = db.Periods.Where(m => m.PeriodID == x.PeriodID).FirstOrDefault();
                        var incometype = db.IncomeTypes.Where(m => m.IncomeTypeID == x.IncomeTypeID).FirstOrDefault();

                        otherIncome.OtherIncomeID = x.OtherIncomeID;
                        otherIncome.IncomeType = incometype.Description;
                        otherIncome.StDate = x.StDate;
                        otherIncome.ToDate = x.ToDate;
                        otherIncome.Period = period.PeriodDescription;
                        otherIncome.Amount = x.Amount;

                        lOtherIncome.Add(otherIncome);
                    }

                    datagridview.ItemsSource = lOtherIncome.OrderBy(m => m.OtherIncomeID);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetOtherIncomes();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddOtherIncome addOtherIncome = new AddOtherIncome();
            addOtherIncome.mode = 1;
            addOtherIncome.empid = empid;
            addOtherIncome.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetOtherIncomes();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((DMSIClass._OtherIncome)datagridview.SelectedItem);
            AddOtherIncome addOtherIncome = new AddOtherIncome();
            addOtherIncome.otherincid = x.OtherIncomeID;
            addOtherIncome.mode = 2;
            addOtherIncome.empid = empid;
            addOtherIncome.Show();
        }

    }
}
