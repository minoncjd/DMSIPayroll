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
    /// Interaction logic for AddLoanType.xaml
    /// </summary>
    public partial class AddLoanType : MetroWindow  
    {
        public int mode;
        public int loantypeid;
        public AddLoanType()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (mode == 2)
            {
                this.Title = "UPDATE LOAN TYPE";
                GetLoanType();
            }
        }

        private void GetLoanType()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var loanType = db.LoanTypes.Where(m => m.LoanTypeID == loantypeid).FirstOrDefault();
                    tbDescription.Text = loanType.Description;
                    tbLoanTypeCode.Text = loanType.LoanTypeCode;
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
                    if (tbDescription.Text == "" || tbLoanTypeCode.Text == "")
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (mode == 1)
                    {
                        LoanType loanType = new LoanType();
                        loanType.Description = tbDescription.Text;
                        loanType.LoanTypeCode = tbLoanTypeCode.Text;
                        db.LoanTypes.Add(loanType);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var loanType = db.LoanTypes.Where(m => m.LoanTypeID == loantypeid).FirstOrDefault();
                        loanType.Description = tbDescription.Text;
                        loanType.LoanTypeCode = tbLoanTypeCode.Text;
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
            tbDescription.Text = "";
            tbLoanTypeCode.Text = "";
        }
    }
}
