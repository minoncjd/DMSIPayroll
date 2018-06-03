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
    /// Interaction logic for AddLoan.xaml
    /// </summary>
    public partial class AddLoan : MetroWindow
    {
        public int mode;
        public int empid;
        public int loanid;
        public AddLoan()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    cbLoanType.ItemsSource = db.LoanTypes.OrderBy(m => m.Description).ToList();
                    cbLoanType.DisplayMemberPath = "Description";
                    cbLoanType.SelectedValuePath = "LoanTypeID";

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
                this.Title = "UPDATE LOAN";
                GetLoan();
            }
        }

        private void GetLoan()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var loan = db.Loans.Where(m => m.LoanID == loanid).FirstOrDefault();
                    tbAmortization.Text = loan.Amortization.ToString("G29");
                    tbPrincipalAmount.Text = loan.PrincipalAmount.ToString("G29");
                    tbTotalLoanAmount.Text = loan.TotalLoanAmount.ToString("G29");
                    tbTotalPayment.Text = loan.TotalPayment.ToString("G29");
                    cbLoanType.SelectedValue = loan.LoanTypeID;
                    cbPeriod.SelectedValue = loan.PeriodID;
                    dpStartDate.SelectedDate = loan.StDate;
                    dpToDate.SelectedDate = loan.ToDate;      
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

                    if (tbAmortization.Text == "" || tbPrincipalAmount.Text == "" || tbTotalLoanAmount.Text == "" || tbTotalPayment.Text == "" || cbLoanType.SelectedItem == null || cbPeriod.SelectedItem == null || dpStartDate.SelectedDate == null ||dpToDate.SelectedDate == null )
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (mode == 1)
                    {
                        Loan loan = new Loan();
                        loan.EmployeeID = empid;
                        loan.Amortization = Decimal.Parse(tbAmortization.Text);
                        loan.PrincipalAmount = Decimal.Parse(tbPrincipalAmount.Text);
                        loan.TotalLoanAmount = Decimal.Parse(tbTotalLoanAmount.Text);
                        loan.TotalPayment = Decimal.Parse(tbTotalPayment.Text);
                        loan.StDate = dpStartDate.SelectedDate.Value;
                        loan.ToDate = dpToDate.SelectedDate.Value;
                        loan.LoanTypeID = Convert.ToInt32(cbLoanType.SelectedValue);
                        loan.PeriodID = Convert.ToInt32(cbPeriod.SelectedValue);
                        db.Loans.Add(loan);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var loan = db.Loans.Where(m => m.LoanID == loanid).FirstOrDefault();
                        loan.EmployeeID = empid;
                        loan.Amortization = Decimal.Parse(tbAmortization.Text);
                        loan.PrincipalAmount = Decimal.Parse(tbPrincipalAmount.Text);
                        loan.TotalLoanAmount = Decimal.Parse(tbTotalLoanAmount.Text);
                        loan.TotalPayment = Decimal.Parse(tbTotalPayment.Text);
                        loan.StDate = dpStartDate.SelectedDate.Value;
                        loan.ToDate = dpToDate.SelectedDate.Value;
                        loan.LoanTypeID = Convert.ToInt32(cbLoanType.SelectedValue);
                        loan.PeriodID = Convert.ToInt32(cbPeriod.SelectedValue);
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
            tbPrincipalAmount.Text = "";
            tbTotalLoanAmount.Text = "";
            tbTotalPayment.Text = "";
            cbLoanType.Text = "";
            cbPeriod.Text = "";
            dpStartDate.SelectedDate = null;
            dpToDate.SelectedDate = null;
            
        }
    }
}
