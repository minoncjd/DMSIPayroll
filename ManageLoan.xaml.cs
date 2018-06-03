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
    /// Interaction logic for ManageLoan.xaml
    /// </summary>
    public partial class ManageLoan : MetroWindow
    {
        List<DMSIClass._Loan> lLoan = new List<DMSIClass._Loan>();
        public int empid;
        public ManageLoan()
        {
            InitializeComponent();
        }

        private void GetLoans()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    lLoan = new List<DMSIClass._Loan>();
                    var loans = db.Loans.Where(m=>m.EmployeeID == empid).ToList();

                    foreach (var x in loans)
                    {
                        DMSIClass._Loan loan = new DMSIClass._Loan();
                        var period = db.Periods.Where(m => m.PeriodID == x.PeriodID).FirstOrDefault();
                        var loantype = db.LoanTypes.Where(m => m.LoanTypeID == x.LoanTypeID).FirstOrDefault();

                        loan.LoanID = x.LoanID;
                        loan.LoanType = loantype.Description;
                        loan.StDate = x.StDate;
                        loan.ToDate = x.ToDate;
                        loan.Period = period.PeriodDescription;
                        loan.PrincipalAmount = x.PrincipalAmount;
                        loan.LoanAmount = x.TotalLoanAmount;
                        loan.LoanPayment = x.TotalPayment;
                        loan.LoanBalance = x.LoanBalance;
                        loan.Amortization = x.Amortization;


                        lLoan.Add(loan);
                    }

                    datagridview.ItemsSource = lLoan.OrderBy(m => m.LoanID);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetLoans();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddLoan addLoan = new AddLoan();
            addLoan.mode = 1;
            addLoan.empid = empid;
            addLoan.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetLoans();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((DMSIClass._Loan)datagridview.SelectedItem);
            AddLoan addLoan = new AddLoan();
            addLoan.loanid = x.LoanID;
            addLoan.mode = 2;
            addLoan.empid = empid;

            addLoan.Show();
        }

        private void datagridview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   

        }
    }
}
