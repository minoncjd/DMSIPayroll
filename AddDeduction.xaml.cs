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
    /// Interaction logic for AddDeduction.xaml
    /// </summary>
    public partial class AddDeduction : MetroWindow
    {
        public int mode;
        public int empid;
        public int deductionid;
        public AddDeduction()
        {
            InitializeComponent();
        }


        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    cbDeductionType.ItemsSource = db.DeductionTypes.OrderBy(m => m.Description).ToList();
                    cbDeductionType.DisplayMemberPath = "Description";
                    cbDeductionType.SelectedValuePath = "DeductionTypeID";

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
                this.Title = "UPDATE DEDUCTION";
                GetDeduction();
            }
        }

        private void GetDeduction()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var deduction = db.Deductions.Where(m => m.DeductionID == deductionid).FirstOrDefault();
                    tbAmortization.Text = deduction.Amortization.ToString("G29");
                    cbDeductionType.SelectedValue = deduction.DeductionTypeID;
                    cbPeriod.SelectedValue = deduction.PeriodID;
                    dpStartDate.SelectedDate = deduction.StDate;
                    dpToDate.SelectedDate = deduction.ToDate;
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

                    if (cbDeductionType.SelectedItem == null || dpStartDate.SelectedDate == null || dpToDate.SelectedDate == null || cbPeriod.SelectedItem == null || tbAmortization.Text == "")
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (mode == 1)
                    {
                        Deduction deduction = new Deduction();
                        deduction.EmployeeID = empid;
                        deduction.Amortization = Decimal.Parse(tbAmortization.Text);
                        deduction.StDate = dpStartDate.SelectedDate.Value;
                        deduction.ToDate = dpToDate.SelectedDate.Value;
                        deduction.DeductionTypeID = Convert.ToInt32(cbDeductionType.SelectedValue);
                        deduction.PeriodID = Convert.ToInt32(cbPeriod.SelectedValue);
                        db.Deductions.Add(deduction);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var deduction = db.Deductions.Where(m => m.DeductionID == deductionid).FirstOrDefault();
                        deduction.EmployeeID = empid;
                        deduction.Amortization = Decimal.Parse(tbAmortization.Text);
                        deduction.StDate = dpStartDate.SelectedDate.Value;
                        deduction.ToDate = dpToDate.SelectedDate.Value;
                        deduction.DeductionTypeID = Convert.ToInt32(cbDeductionType.SelectedValue);
                        deduction.PeriodID = Convert.ToInt32(cbPeriod.SelectedValue);
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
            tbAmortization.Text = "";
            cbDeductionType.Text = "";
            cbPeriod.Text = "";
            dpStartDate.SelectedDate = null;
            dpToDate.SelectedDate = null;
        }
    }
}
