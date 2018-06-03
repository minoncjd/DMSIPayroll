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
    /// Interaction logic for ManageEmployee.xaml
    /// </summary>
    public partial class ManageEmployee : MetroWindow
    {
        List<DMSIClass._Employee> lEmployee = new List<DMSIClass._Employee>();
        public ManageEmployee()
        {
            InitializeComponent();
        }

        private void GetEmployees()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    lEmployee = new List<DMSIClass._Employee>();
                    var employees = db.Employees.ToList();

                    foreach (var x in employees)
                    {
                        DMSIClass._Employee employee = new DMSIClass._Employee();
                        var company = db.Companies.Where(m => m.CompanyID == x.CompanyID).FirstOrDefault();
                        var position = db.EmployeePositions.Where(m => m.EmployeePositionID == x.EmployeePositionID).FirstOrDefault();

                        employee.Name = (x.LastName + ", " + x.FirstName + " " + x.MiddleName).ToUpper();
                        employee.Company = company.CompanyName;
                        employee.Position = position.PositionName;
                        employee.EmployeeID = x.EmployeeID;
                        employee.EmployeeNo = x.EmployeeNumber;
                        employee.IsActive = x.IsActive;
                        lEmployee.Add(employee);
                    }

                    datagridview.ItemsSource = lEmployee.OrderBy(m => m.Name);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetEmployees();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            addEmployee.mode = 1;
            addEmployee.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetEmployees();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((DMSIClass._Employee)datagridview.SelectedItem);
            AddEmployee addEmployee = new AddEmployee();
            addEmployee.employeeid = x.EmployeeID;
            addEmployee.mode = 2;
            addEmployee.Show();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var search = tbSearch.Text.ToUpper();
                datagridview.ItemsSource = lEmployee.Where(m => m.Name.Contains(search)).OrderBy(m => m.Name);
            }
        }
    }
}
