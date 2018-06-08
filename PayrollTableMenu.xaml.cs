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
    /// Interaction logic for PayrollListMenu.xaml
    /// </summary>
    public partial class PayrollTableMenu : MetroWindow
    {
        public PayrollTableMenu()
        {
            InitializeComponent();
        }

        private void btnPayrollOtherDivision_Click(object sender, RoutedEventArgs e)
        {
            PayrollTableList payrollTableList = new PayrollTableList();
            payrollTableList.pytype = 2;
            payrollTableList.Show();
        }

        private void btnPayrollLogistics_Click(object sender, RoutedEventArgs e)
        {
            PayrollTableList payrollTableList = new PayrollTableList();
            payrollTableList.pytype = 1;
            payrollTableList.Show();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
