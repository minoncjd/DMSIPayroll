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
    /// Interaction logic for AddIncomeLogistics.xaml
    /// </summary>
    public partial class AddIncomeLogistics : MetroWindow
    {
        public int employeeid;
        public int mode;
        public int incomeid;

        public AddIncomeLogistics()
        {
            InitializeComponent();
        }

        private void GetInfo()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var info = db.Incomes.Where(m => m.IncomeID == incomeid).FirstOrDefault();
                    var emp = db.Employees.Where(m => m.EmployeeID == employeeid).FirstOrDefault();
                    tbAmount.Text = info.Amount.ToString("G29");
                    tbEmployee.Text = emp.LastName.ToUpper() + ", " + emp.FirstName.ToUpper();
                    dpToDate.SelectedDate = info.ToDate;
                    dpStDate.SelectedDate = info.StDate;
                    dpStartDate.SelectedDate = info.PayrollDate;
                    tbNoOfDays.Text = info.NoOfDays.ToString("G29");
                    tbAmount.Text = info.Amount.ToString("G29");

                    if (info.Trip == 1)
                    {
                        rb1st.IsChecked = true;
                    }
                    else
                    {
                        rb2nd.IsChecked = true;
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

                    var emp = db.Employees.Where(m => m.EmployeeID == employeeid).FirstOrDefault();
                    var pos = db.EmployeePositions.Where(m => m.EmployeePositionID == emp.EmployeePositionID).FirstOrDefault();

                    tbEmployee.Text = (emp.LastName + ", " + emp.FirstName + " " + emp.MiddleName).ToUpper();
                    tbDailyRate.Text = pos.DailyRate.ToString("G29");

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            if (mode == 2)
            {
                this.Title = "UPDATE INFO";
                GetInfo();

            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    if (tbNoOfDays.Text == "" || tbAmount.Text == "" || dpStDate.SelectedDate == null || dpToDate.SelectedDate == null || dpToDate.SelectedDate == null || rb1st.IsChecked == false && rb2nd.IsChecked == false)
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;

                    }
                    if (mode == 1)
                    {
                        Income income = new Income();
                        income.EmployeeID = employeeid;
                        income.PayrollDate = dpStartDate.SelectedDate.Value;
                        income.Amount = Decimal.Parse(tbAmount.Text);
                        income.NoOfDays = Decimal.Parse(tbNoOfDays.Text);
                        income.StDate = dpStartDate.SelectedDate.Value;
                        income.ToDate = dpToDate.SelectedDate.Value;
                
                        if (rb1st.IsChecked == true)
                        {
                            income.Trip = 1;
                        }
                        else
                        {
                            income.Trip = 2;
                        }
                        db.Incomes.Add(income);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var income = db.Incomes.Where(m => m.IncomeID == incomeid).FirstOrDefault();
                        income.EmployeeID = employeeid;
                        income.PayrollDate = dpStartDate.SelectedDate.Value;
                        income.NoOfDays = Decimal.Parse(tbNoOfDays.Text);
                        income.Amount = Decimal.Parse(tbAmount.Text);
                        income.StDate = dpStDate.SelectedDate.Value;
                        income.ToDate = dpToDate.SelectedDate.Value;

                        if (rb1st.IsChecked == true)
                        {
                            income.Trip = 1;
                        }
                        else
                        {
                            income.Trip = 2;
                        }
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

        private void TotalAmount()
        {
            double dailyrate = Convert.ToDouble(tbDailyRate.Text);
            var noofdays = Convert.ToInt32(tbNoOfDays.Text);
            tbAmount.Text = (dailyrate * noofdays).ToString();
        }

        private void tbNoOfDays_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbNoOfDays.Text))
            {
                return;
            }
            else
            {
                TotalAmount();
            }
        }

        private void clear()
        {
            tbAmount.Text = "";
            tbNoOfDays.Text = "";
            dpStartDate.SelectedDate = null;
            dpToDate.SelectedDate = null;
            dpStDate.SelectedDate = null;
            rb1st.IsChecked = false;
            rb2nd.IsChecked = false;
        }
    }
}
