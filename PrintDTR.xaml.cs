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
    /// Interaction logic for PrintDTR.xaml
    /// </summary>
    public partial class PrintDTR : MetroWindow
    {
        public PrintDTR()
        {
            InitializeComponent();
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
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    if (rbCompany.IsChecked == false && rbPosition.IsChecked== false)
                    {
                        MessageBox.Show("Select Report Type.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    var startDate = dpstartDate.SelectedDate.Value.ToShortDateString();
                    var endDate = dpToDate.SelectedDate.Value.ToShortDateString();
                    var companyid = Convert.ToInt32(cbCompany.SelectedValue);
                    var positionid = Convert.ToInt32(cbPosition.SelectedValue);
                    List<Employee> employees = new List<Employee>();
                    List<GetEmployeeDTR_Result> lResult = new List<GetEmployeeDTR_Result>();
                    if (rbCompany.IsChecked == true)
                    {
                        employees = db.Employees.Where(m => m.CompanyID == companyid && m.BiometricsID != null).ToList();
                    }
                    else if (rbPosition.IsChecked == true)
                    {
                        employees = db.Employees.Where(m => m.EmployeePositionID == positionid && m.BiometricsID != null).ToList();
                    }
                    foreach (var x in employees)
                    {
                        var qry = from a in db.GetEmployeeDTR(startDate, endDate, x.BiometricsID)
                                  select a;
                        lResult.AddRange(qry);
                    }

                    PrintWindow printWindow = new PrintWindow();
                    printWindow.rptid = 1;
                    printWindow.startDate = startDate;
                    printWindow.endDate = endDate;
                    printWindow.Report1 = lResult;
                    printWindow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
                //MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
