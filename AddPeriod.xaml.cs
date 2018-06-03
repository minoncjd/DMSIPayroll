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
    /// Interaction logic for AddPYPeriod.xaml
    /// </summary>
    public partial class AddPeriod : MetroWindow
    {

        public int mode;
        public int periodid;    
        public AddPeriod()
        {
            InitializeComponent();
        }

        private void GetPeriod()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var period = db.Periods.Where(m => m.PeriodID == periodid).FirstOrDefault();
                    tbDescription.Text = period.PeriodDescription;
                    tbPeriodCode.Text = period.PeriodCode;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (mode ==2)
            {
                this.Title = "UPDATE PERIOD";
                GetPeriod();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    if (tbDescription.Text == "" || tbPeriodCode.Text == "")
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (mode == 1)
                    {
                        Period period = new Period();
                        period.PeriodCode = tbPeriodCode.Text;
                        period.PeriodDescription = tbDescription.Text;
                        db.Periods.Add(period);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var period = db.Periods.Where(m => m.PeriodID == periodid).FirstOrDefault();
                        period.PeriodCode = tbPeriodCode.Text;
                        period.PeriodDescription = tbDescription.Text;
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
            tbPeriodCode.Text = "";
        }
    }
}
