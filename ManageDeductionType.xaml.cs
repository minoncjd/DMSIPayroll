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
    /// Interaction logic for ManageDeductionType.xaml
    /// </summary>
    public partial class ManageDeductionType : MetroWindow
    {
        List<DeductionType> lDeductionType = new List<DeductionType>();

        public ManageDeductionType()
        {
            InitializeComponent();
        }

        private void GetDeductionType()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    lDeductionType = db.DeductionTypes.ToList();
                    datagridview.ItemsSource = lDeductionType.OrderBy(m => m.Description);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetDeductionType();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddDeductionType addDeductionType = new AddDeductionType();
            addDeductionType.mode = 1;
            addDeductionType.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetDeductionType();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((DeductionType)datagridview.SelectedItem);
            AddDeductionType addDeductionType = new AddDeductionType();
            addDeductionType.deductiontypeid = x.DeductionTypeID;
            addDeductionType.mode = 2;
            addDeductionType.Show();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var search = tbSearch.Text.Trim();
                datagridview.ItemsSource = lDeductionType.Where(m => m.Description.Contains(search)).OrderBy(m => m.Description);
            }
        }
    }
}
