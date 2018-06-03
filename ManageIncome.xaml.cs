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
    /// Interaction logic for ManageIncome.xaml
    /// </summary>
    public partial class ManageIncome : MetroWindow
    {
        public int empid;
        public int payrolltype;

        Employee emp = new Employee();
        List<DMSIClass._Income> lIncome = new List<DMSIClass._Income>();
        public ManageIncome()
        {
            InitializeComponent();
        }

        private void GetIncomes()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    lIncome = new List<DMSIClass._Income>();
                    var incomes = db.Incomes.Where(m => m.EmployeeID == empid).ToList();
                    emp = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();

                    foreach (var x in incomes)
                    {
                        DMSIClass._Income income = new DMSIClass._Income();
                       
                        var position = db.EmployeePositions.Where(m => m.EmployeePositionID == emp.EmployeePositionID).FirstOrDefault();

                        income.PayrollDate = x.PayrollDate;
                        income.DailyRate = position.DailyRate;
                        income.NoOfDays = Convert.ToInt32(x.NoOfDays);
                        income.Amount = x.Amount;       
                        income.IncomeID = x.IncomeID;
                        income.IncomePeriod = x.StDate.ToShortDateString() + " - " + x.ToDate.ToShortDateString();
                        income.PayrollID = Convert.ToInt32(x.PayrollID);
                        lIncome.Add(income);
                    }

                    datagridview.ItemsSource = lIncome.OrderByDescending(m => m.IncomeID);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetIncomes();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
  
         
            AddIncome addIncome = new AddIncome();
            addIncome.mode = 1;
            addIncome.employeeid = empid;
            addIncome.Show();
          
        
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetIncomes();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {

           
            var x = ((DMSIClass._Income)datagridview.SelectedItem);
            AddIncome addIncome = new AddIncome();
            addIncome.incomeid = x.IncomeID;
            addIncome.employeeid = empid;
            addIncome.mode = 2;
            addIncome.Show();
            
          
        }

        private void datagridview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void datagridview_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //if (Convert.ToInt32(((DMSIClass._Income)(e.Row.DataContext)).PayrollID) == 0 )
            //{
            //    e.Row.Background = new SolidColorBrush(Color.FromRgb(127, 140, 141));
            //}
        }
    }
}
