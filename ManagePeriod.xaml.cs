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
    /// Interaction logic for ManagePeriod.xaml
    /// </summary>
    public partial class ManagePeriod : MetroWindow
    {
        List<Period> lPeriod = new List<Period>();
        public ManagePeriod()
        {
            InitializeComponent();
        }

        private void GetPeriods()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    lPeriod = db.Periods.ToList();
                    datagridview.ItemsSource = lPeriod.OrderBy(m => m.PeriodDescription);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetPeriods();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddPeriod addPeriod = new AddPeriod();
            addPeriod.mode = 1;
            addPeriod.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetPeriods();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((Period)datagridview.SelectedItem);
            AddPeriod addPeriod = new AddPeriod();
            addPeriod.periodid = x.PeriodID;
            addPeriod.mode = 2;
            addPeriod.Show();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var search = tbSearch.Text.Trim();
                datagridview.ItemsSource = lPeriod.Where(m => m.PeriodDescription.Contains(search)).OrderBy(m => m.PeriodDescription);
            }
        }
    }
}
