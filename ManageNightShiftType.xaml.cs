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
    /// Interaction logic for ManageNightShiftType.xaml
    /// </summary>
    public partial class ManageNightShiftType : MetroWindow
    {
        List<NightShiftType> lNightShiftType = new List<NightShiftType>();

        public ManageNightShiftType()
        {
            InitializeComponent();
        }

        private void GetNightShiftType()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    lNightShiftType = db.NightShiftTypes.ToList();
                    datagridview.ItemsSource = lNightShiftType.OrderBy(m => m.Description);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetNightShiftType();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddNightShiftType addNightShiftType = new AddNightShiftType();
            addNightShiftType.mode = 1;
            addNightShiftType.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetNightShiftType();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((NightShiftType)datagridview.SelectedItem);
            AddNightShiftType addNightShiftType = new AddNightShiftType();
            addNightShiftType.nightshifttypeid = x.NightShiftTypeID;
            addNightShiftType.mode = 2;
            addNightShiftType.Show();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var search = tbSearch.Text.Trim();
                datagridview.ItemsSource = lNightShiftType.Where(m => m.Description.Contains(search)).OrderBy(m => m.Description);
            }
        }
    }
}
