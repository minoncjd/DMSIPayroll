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
    /// Interaction logic for AddOtherIncome.xaml
    /// </summary>
    public partial class AddOtherIncome : MetroWindow
    {

        public int mode;
        public int empid;
        public int otherincid;

        public AddOtherIncome()
        {
            InitializeComponent();
        }


        private void GetLoan()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var otherinc = db.OtherIncomes.Where(m => m.OtherIncomeID == otherincid).FirstOrDefault();
                    tbAmount.Text = otherinc.Amount.ToString("G29");
                    cbIncomeType.SelectedValue = otherinc.IncomeTypeID;
                    cbPeriod.SelectedValue = otherinc.PeriodID;
                    dpStartDate.SelectedDate = otherinc.StDate;
                    dpToDate.SelectedDate = otherinc.ToDate;
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

                    if (tbAmount.Text == "" || cbIncomeType.SelectedItem == null || cbPeriod.SelectedItem == null || dpStartDate.SelectedDate == null || dpToDate.SelectedDate == null)
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (mode == 1)
                    {
                        OtherIncome otherIncome = new OtherIncome();
                        otherIncome.EmployeeID = empid;
                        otherIncome.Amount = Decimal.Parse(tbAmount.Text);
                        otherIncome.StDate = dpStartDate.SelectedDate.Value;
                        otherIncome.ToDate = dpToDate.SelectedDate.Value;
                        otherIncome.IncomeTypeID = Convert.ToInt32(cbIncomeType.SelectedValue);
                        otherIncome.PeriodID = Convert.ToInt32(cbPeriod.SelectedValue);
                        db.OtherIncomes.Add(otherIncome);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var otherIncome = db.OtherIncomes.Where(m => m.OtherIncomeID == otherincid).FirstOrDefault();
                        otherIncome.EmployeeID = empid;
                        otherIncome.Amount = Decimal.Parse(tbAmount.Text);
                        otherIncome.StDate = dpStartDate.SelectedDate.Value;
                        otherIncome.ToDate = dpToDate.SelectedDate.Value;
                        otherIncome.IncomeTypeID = Convert.ToInt32(cbIncomeType.SelectedValue);
                        otherIncome.PeriodID = Convert.ToInt32(cbPeriod.SelectedValue);

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

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    cbIncomeType.ItemsSource = db.IncomeTypes.OrderBy(m => m.Description).ToList();
                    cbIncomeType.DisplayMemberPath = "Description";
                    cbIncomeType.SelectedValuePath = "IncomeTypeID";

                    cbPeriod.ItemsSource = db.Periods.OrderBy(m => m.PeriodDescription).ToList();
                    cbPeriod.DisplayMemberPath = "PeriodDescription";
                    cbPeriod.SelectedValuePath = "PeriodID";

                    var emp = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();
                    tbEmployee.Text = (emp.LastName + ", " + emp.FirstName + " " + emp.MiddleName).ToUpper();

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }


            if (mode == 2)
            {
                this.Title = "UPDATE OTHER INCOME";
                GetLoan();
            }
        }

        private void clear()
        {
            tbAmount.Text = "";
            cbIncomeType.Text = "";
            cbPeriod.Text = "";
            dpStartDate.SelectedDate = null;
            dpToDate.SelectedDate = null;

        }

    }
}
