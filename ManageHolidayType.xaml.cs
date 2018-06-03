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
    /// Interaction logic for ManageHolidayType.xaml
    /// </summary>
    public partial class ManageHolidayType : MetroWindow
    {
        List<HolidayType> lHolidayType = new List<HolidayType>();
        public ManageHolidayType()
        {
            InitializeComponent();
        }

        private void GetHolidayType()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    lHolidayType = db.HolidayTypes.ToList();
                    datagridview.ItemsSource = lHolidayType.OrderBy(m => m.Description);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetHolidayType();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddHolidayType addHolidayType = new AddHolidayType();
            addHolidayType.mode = 1;
            addHolidayType.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetHolidayType();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((HolidayType)datagridview.SelectedItem);
            AddHolidayType addHolidayType = new AddHolidayType();
            addHolidayType.holidaytypeid = x.HolidayTypeID;
            addHolidayType.mode = 2;
            addHolidayType.Show();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var search = tbSearch.Text.Trim();
                datagridview.ItemsSource = lHolidayType.Where(m => m.Description.Contains(search)).OrderBy(m => m.Description);
            }
        }

    }
}
