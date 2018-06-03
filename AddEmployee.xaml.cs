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
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : MetroWindow
    {
        public int mode;
        public int employeeid;

        
        public AddEmployee()
        {
            InitializeComponent();
        }
        
        private void GetEmployee()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var employee = db.Employees.Where(m => m.EmployeeID == employeeid).FirstOrDefault();
                    tbEmployeeNumber.Text = employee.EmployeeNumber;
                    tbLastName.Text = employee.LastName;
                    tbFirstName.Text = employee.FirstName;
                    tbMiddleName.Text = employee.MiddleName;
                    cbCompany.SelectedValue = employee.CompanyID;
                    cbPosition.SelectedValue = employee.EmployeePositionID;
                    tbBiometricsID.Text = employee.BiometricsID.ToString();
                    tbAddress.Text = employee.Address;
                    dpDateHired.SelectedDate = employee.DateHired;
                    dpBirthdate.SelectedDate = employee.Birthdate;
                    dpDateResigned.SelectedDate = employee.DateResigned;
                    if (employee.PayrollType == 1)
                    {
                        rbLogistics.IsChecked = true;
                    }
                    else
                    {
                        rbOtherDivision.IsChecked = true;
                    }

                    if (employee.IsActive == true)
                    {
                        cboxIsActive.IsChecked = true;
                    }
                    else
                    {
                        cboxIsActive.IsChecked = false;
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
                    cbCompany.ItemsSource = db.Companies.OrderBy(m => m.CompanyName).ToList();
                    cbCompany.DisplayMemberPath = "CompanyName";
                    cbCompany.SelectedValuePath = "CompanyID";

                    cbPosition.ItemsSource = db.EmployeePositions.OrderBy(m => m.PositionName).ToList();
                    cbPosition.DisplayMemberPath = "PositionName";
                    cbPosition.SelectedValuePath = "EmployeePositionID";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            if (mode == 2)
            {
                this.Title = "UPDATE EMPLOYEE";
                GetEmployee();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {

                    if (tbEmployeeNumber.Text == "" || tbFirstName.Text == "" || tbLastName.Text == "" || cbCompany.SelectedItem == null || cbPosition.SelectedItem == null)
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (mode == 1)
                    {
                        Employee employee = new Employee();
                        employee.LastName = tbLastName.Text;
                        employee.FirstName = tbFirstName.Text;
                        employee.MiddleName = tbMiddleName.Text;
                        employee.EmployeePositionID = Convert.ToInt32(cbPosition.SelectedValue);
                        employee.CompanyID = Convert.ToInt32(cbCompany.SelectedValue);
                        employee.DateResigned = dpDateResigned.SelectedDate == null ? null : dpDateResigned.SelectedDate;
                        employee.EmployeeNumber = tbEmployeeNumber.Text;
                        employee.BiometricsID = tbBiometricsID.Text == "" ? (int?)null : Convert.ToInt32(tbBiometricsID.Text);
                        employee.DateHired = dpDateHired.SelectedDate == null ? null : dpDateHired.SelectedDate;
                        employee.Birthdate = dpBirthdate.SelectedDate == null ? null : dpBirthdate.SelectedDate;
                        employee.Address = tbAddress.Text;
                        if (rbLogistics.IsChecked == true)
                        {
                            employee.PayrollType = 1;
                        }
                        else
                        {
                            employee.PayrollType = 2;
                        }
                        db.Employees.Add(employee);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var employee = db.Employees.Where(m => m.EmployeeID == employeeid).FirstOrDefault();
                        employee.LastName = tbLastName.Text;
                        employee.FirstName = tbFirstName.Text;
                        employee.MiddleName = tbMiddleName.Text;
                        employee.EmployeePositionID = Convert.ToInt32(cbPosition.SelectedValue);
                        employee.CompanyID = Convert.ToInt32(cbCompany.SelectedValue);
                        employee.DateResigned = dpDateResigned.SelectedDate == null ? null : dpDateResigned.SelectedDate;
                        employee.EmployeeNumber = tbEmployeeNumber.Text;
                        employee.BiometricsID = tbBiometricsID.Text == "" ? (int?)null : Convert.ToInt32(tbBiometricsID.Text);
                        employee.DateHired = dpDateHired.SelectedDate == null ? null : dpDateHired.SelectedDate;
                        employee.Birthdate = dpBirthdate.SelectedDate == null ? null : dpBirthdate.SelectedDate;
                        employee.Address = tbAddress.Text;
                        employee.IsActive = cboxIsActive.IsChecked == false ? false : true;
                        if (rbLogistics.IsChecked == true)
                        {
                            employee.PayrollType = 1;
                        }
                        else
                        {
                            employee.PayrollType = 2;
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

        private void clear()
        {
            tbBiometricsID.Text = "";
            tbEmployeeNumber.Text = "";
            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbMiddleName.Text = "";
            cbCompany.Text = "";
            cbPosition.Text = "";
            dpDateResigned.SelectedDate = null;
            dpBirthdate.SelectedDate = null;
            dpDateHired.SelectedDate = null;
            tbAddress.Text = "";
        }
    }
}
