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
    /// Interaction logic for ManageCompany.xaml
    /// </summary>
    public partial class ManageCompany : MetroWindow
    {
        List<Company> lCompany = new List<Company>();
        public ManageCompany()
        {
            InitializeComponent();
        }

        private void GetCompanies()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    lCompany = db.Companies.ToList();
                    datagridview.ItemsSource = lCompany.OrderBy(m => m.CompanyName);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }


        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetCompanies();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddCompany addCompany = new AddCompany();
            addCompany.mode = 1;
            addCompany.ShowDialog();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetCompanies();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((Company)datagridview.SelectedItem);
            AddCompany addCompany = new AddCompany();
            addCompany.companyid = x.CompanyID;
            addCompany.mode = 2;
            addCompany.Show();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var search = tbSearch.Text.Trim();
                datagridview.ItemsSource = lCompany.Where(m => m.CompanyName.Contains(search)).OrderBy(m => m.CompanyName);
            }
            
        }
    }
}
