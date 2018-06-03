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
    /// Interaction logic for AddCompany.xaml
    /// </summary>
    public partial class AddCompany : MetroWindow
    {
        public int mode;
        public int companyid;

        public AddCompany()
        {
            InitializeComponent();
        }

        private void Getcompany()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var company = db.Companies.Where(m => m.CompanyID == companyid).FirstOrDefault();
                    tbCompany.Text = company.CompanyName;
                    tbAddress.Text = company.Address;
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
                    if (tbCompany.Text == "" || tbAddress.Text == "")
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (mode == 1)
                    {
                        Company company = new Company();
                        company.CompanyName = tbCompany.Text;
                        company.Address = tbAddress.Text;
                        db.Companies.Add(company);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var company = db.Companies.Where(m => m.CompanyID == companyid).FirstOrDefault();
                        company.CompanyName = tbCompany.Text;
                        company.Address = tbAddress.Text;
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

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (mode == 2)
            {
                this.Title = "UPDATE COMPANY";
                Getcompany();
            }
        }

        private void clear()
        {
            tbAddress.Text = "";
            tbCompany.Text = "";
        }
    }
}
