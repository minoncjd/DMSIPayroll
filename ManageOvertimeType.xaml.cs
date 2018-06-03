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
    /// Interaction logic for ManageAdjustmentType.xaml
    /// </summary>
    public partial class ManageOvertimeType : MetroWindow
    {
        List<OvertimeType> lOvertimeType = new List<OvertimeType>();
        public ManageOvertimeType()
        {
            InitializeComponent();
        }

        private void GetAdjustmentTypes()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    lOvertimeType = db.OvertimeTypes.ToList();
                    datagridview.ItemsSource = lOvertimeType.OrderBy(m => m.Description);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetAdjustmentTypes();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddOvertimeType addOvertimeType = new AddOvertimeType();
            addOvertimeType.mode = 1;
            addOvertimeType.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetAdjustmentTypes();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((OvertimeType)datagridview.SelectedItem);
            AddOvertimeType addOvertimeType = new AddOvertimeType();
            addOvertimeType.overtimetypeid = x.OvertimeTypeID;
            addOvertimeType.mode = 2;
            addOvertimeType.Show();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var search = tbSearch.Text.Trim();
                datagridview.ItemsSource = lOvertimeType.Where(m => m.Description.Contains(search)).OrderBy(m => m.Description);
            }
        }
    }
}
