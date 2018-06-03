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
    /// Interaction logic for ManageIncomeType.xaml
    /// </summary>
    public partial class ManageIncomeType : MetroWindow
    {
        List<IncomeType> lIncomeType = new List<IncomeType>();
        public ManageIncomeType()
        {
            InitializeComponent();
        }

        private void GetIncomeTypes()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    lIncomeType = db.IncomeTypes.ToList();
                    datagridview.ItemsSource = lIncomeType.OrderBy(m => m.Description);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetIncomeTypes();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddIncomeType addIncomeType = new AddIncomeType();
            addIncomeType.mode = 1;
            addIncomeType.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetIncomeTypes();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((IncomeType)datagridview.SelectedItem);
            AddIncomeType addIncomeType = new AddIncomeType();
            addIncomeType.incometypeid = x.IncomeTypeID;
            addIncomeType.mode = 2;
            addIncomeType.Show();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var search = tbSearch.Text.Trim();
                datagridview.ItemsSource = lIncomeType.Where(m => m.Description.Contains(search)).OrderBy(m => m.Description);
            }
        }
    }
}
