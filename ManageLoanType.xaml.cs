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
    /// Interaction logic for ManageLoanType.xaml
    /// </summary>
    public partial class ManageLoanType : MetroWindow
    {
        List<LoanType> lLoanType = new List<LoanType>();

        public ManageLoanType()
        {
            InitializeComponent();
        }

        private void GetLoanTypes()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    lLoanType = db.LoanTypes.ToList();
                    datagridview.ItemsSource = lLoanType.OrderBy(m => m.Description);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetLoanTypes();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddLoanType addLoanType = new AddLoanType();
            addLoanType.mode = 1;
            addLoanType.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetLoanTypes();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((LoanType)datagridview.SelectedItem);
            AddLoanType addLoanType = new AddLoanType();
            addLoanType.loantypeid = x.LoanTypeID;
            addLoanType.mode = 2;
            addLoanType.Show();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var search = tbSearch.Text.Trim();
                datagridview.ItemsSource = lLoanType.Where(m => m.Description.Contains(search)).OrderBy(m => m.Description);
            }
        }
    }
}
