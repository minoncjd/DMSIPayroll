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
    /// Interaction logic for AddNightDiffrential.xaml
    /// </summary>
    public partial class AddNightDiffrential : MetroWindow
    {
        public int mode;
        public int nightdiffid;
        public int empid;

        public AddNightDiffrential()
        {
            InitializeComponent();
        }


        private void GetNightDiff()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var nightdiff = db.NightDifferentials.Where(m => m.NightDifferentialID == nightdiffid).FirstOrDefault();
                    tbAmount.Text = nightdiff.Amount.ToString("G29");
                    cbNightShiftType.SelectedValue = nightdiff.NightShiftTypeID;
                    dpDate.SelectedDate = nightdiff.PayrollDate;
                    dpStDate.SelectedDate = nightdiff.StDate;
                    dpToDate.SelectedDate = nightdiff.ToDate;
                    tbValue.Text = nightdiff.Value.ToString("G29");
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
                    var pos = db.EmployeePositions.Where(m => m.EmployeePositionID == emp.EmployeePositionID).FirstOrDefault();

                    tbEmployeeName.Text = (emp.LastName + ", " + emp.FirstName + " " + emp.MiddleName).ToUpper();
                    tbHourlyRate.Text = (pos.DailyRate / 8).ToString("G29");

                    cbNightShiftType.ItemsSource = db.NightShiftTypes.OrderBy(m => m.Description).ToList();
                    cbNightShiftType.DisplayMemberPath = "Description";
                    cbNightShiftType.SelectedValuePath = "NightShiftTypeID";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (mode == 2)
            {
                this.Title = "UPDATE NIGHT DIFFERENTIAL";
                GetNightDiff();
            }
        }



        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {

                    if (cbNightShiftType.SelectedItem == null || tbValue.Text == "" || tbAmount.Text == "" || dpToDate.SelectedDate == null || dpStDate.SelectedDate == null || dpDate.SelectedDate == null)
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }


                    if (mode == 1)
                    {
                        NightDifferential NightDifferential = new NightDifferential();
                        NightDifferential.EmployeeID = empid;
                        NightDifferential.PayrollDate = dpDate.SelectedDate.Value;
                        NightDifferential.StDate = dpStDate.SelectedDate.Value;
                        NightDifferential.ToDate = dpToDate.SelectedDate.Value;
                        NightDifferential.Amount = Decimal.Parse(tbAmount.Text);
                        NightDifferential.NightShiftTypeID = Convert.ToInt32(cbNightShiftType.SelectedValue);
                        NightDifferential.Value = Convert.ToInt32(tbValue.Text);
                        db.NightDifferentials.Add(NightDifferential);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var nightdiff = db.NightDifferentials.Where(m => m.NightDifferentialID == nightdiffid).FirstOrDefault();
                        nightdiff.EmployeeID = empid;
                        nightdiff.PayrollDate = dpDate.SelectedDate.Value;
                        nightdiff.StDate = dpStDate.SelectedDate.Value;
                        nightdiff.ToDate = dpToDate.SelectedDate.Value;
                        nightdiff.Amount = Decimal.Parse(tbAmount.Text);
                        nightdiff.NightShiftTypeID = Convert.ToInt32(cbNightShiftType.SelectedValue);
                        nightdiff.Value = Decimal.Parse(tbValue.Text);
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
            try
            {
                using (var db = new DMSIPayrollEntities())
                {

                    var nightshifttypeid = Convert.ToInt32(cbNightShiftType.SelectedValue);
                    var nightshifttype = db.NightShiftTypes.Where(m => m.NightShiftTypeID == nightshifttypeid).FirstOrDefault();
                    var emp = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();
                    var emppositon = db.EmployeePositions.Where(m => m.EmployeePositionID == emp.EmployeePositionID).FirstOrDefault();
                    var value = Convert.ToInt32(tbValue.Text);


                    var hourlyrate = (emppositon.DailyRate) / 8;
                    var nightdiffpay = (hourlyrate * nightshifttype.Multiplier * 8 * (decimal)0.10) * value;
                    tbAmount.Text = nightdiffpay.ToString();

                }

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void tbNoOfDays_LostFocus(object sender, RoutedEventArgs e)
        {
            TotalAmount();
        }

        private void clear()
        {
            tbAmount.Text = "";
            tbValue.Text = "";
            cbNightShiftType.Text = "";
            dpStDate.SelectedDate = null;
            dpToDate.SelectedDate = null;
            dpDate.SelectedDate = null;

        }
    }
}
