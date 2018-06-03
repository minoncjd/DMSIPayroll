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
    /// Interaction logic for ManagePosition.xaml
    /// </summary>
    public partial class ManagePosition : MetroWindow
    {
        List<EmployeePosition> lPosition = new List<EmployeePosition>();

        public ManagePosition()
        {
            InitializeComponent();
        }

        private void GetPositions()
        {
            try
            {

                using (var db = new DMSIPayrollEntities())
                {
                    lPosition = db.EmployeePositions.ToList();
                    datagridview.ItemsSource = lPosition.OrderBy(m => m.PositionName);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetPositions();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddPosition addPosition = new AddPosition();
            addPosition.mode = 1;
            addPosition.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetPositions();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var x = ((EmployeePosition)datagridview.SelectedItem);
            AddPosition addPosition = new AddPosition();
            addPosition.positionid = x.EmployeePositionID;
            addPosition.mode = 2;
            addPosition.Show();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var search = tbSearch.Text.Trim();
                datagridview.ItemsSource = lPosition.Where(m => m.PositionName.Contains(search)).OrderBy(m => m.PositionName);
            }
        }
    }
}
