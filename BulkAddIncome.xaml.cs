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
    /// Interaction logic for BulkAddIncome.xaml
    /// </summary>
    public partial class BulkAddIncome : MetroWindow
    {
        List<DMSIClass._Employee> lEmployee = new List<DMSIClass._Employee>();
        public BulkAddIncome()
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
                        employee.PositionID = x.EmployeePositionID;

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
            try
            {
                using (var db = new DMSIPayrollEntities())
                {

                    if (tbNoOfDays.Text == "" || dpStartDate.SelectedDate == null || dpStDate.SelectedDate == null || dpToDate.SelectedDate == null)
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    List<CheckBox> checkBoxlist = new List<CheckBox>();
                    FindChildGroup<CheckBox>(datagridview, "checkboxinstance", ref checkBoxlist);
                    var checkeditems = checkBoxlist.Where(m => m.IsChecked == true).ToList();

                    foreach (var c in checkeditems)
                    {
                        Income income = new Income();
                        DMSIClass._Employee x = c.DataContext as DMSIClass._Employee;
                        var position = db.EmployeePositions.Where(m => m.EmployeePositionID == x.PositionID).FirstOrDefault();
                        income.EmployeeID = x.EmployeeID;
                        income.NoOfDays = Convert.ToInt32(tbNoOfDays.Text);
                        income.Amount = income.NoOfDays * position.DailyRate;
                        income.StDate = dpStDate.SelectedDate.Value;
                        income.ToDate = dpToDate.SelectedDate.Value;
                        income.PayrollDate = dpStartDate.SelectedDate.Value;
                        db.Incomes.Add(income);
                        db.SaveChanges();
                    }

                    MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                    clear();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void FindChildGroup<T>(DependencyObject parent, string childName, ref List<T> list) where T : DependencyObject
        {
         

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
              
                var child = VisualTreeHelper.GetChild(parent, i);
                T child_Test = child as T;
                if (child_Test == null)
                {
                    FindChildGroup<T>(child, childName, ref list);
                }
                else
                {                 
                    FrameworkElement child_Element = child_Test as FrameworkElement;

                    if (child_Element.Name == childName)
                    {
                        
                        list.Add(child_Test);
                    }
                    FindChildGroup<T>(child, childName, ref list);
                }
            }

            return;
        }

        private void clear()
        {
            tbNoOfDays.Text = "";
            dpStartDate.SelectedDate = null;
            dpToDate.SelectedDate = null;
            dpStDate.SelectedDate = null;
        }
    }


}
