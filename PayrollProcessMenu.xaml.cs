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
    /// Interaction logic for PayrollProcessMenu.xaml
    /// </summary>
    public partial class PayrollProcessMenu : MetroWindow
    {
        public PayrollProcessMenu()
        {
            InitializeComponent();
        }

        private void btnPayrollLogistics_Click(object sender, RoutedEventArgs e)
        {
            PayrollProcessLogistics payrollProcessLogistics = new PayrollProcessLogistics();
            payrollProcessLogistics.Show();
        }

        private void btnPayrollOtherDivision_Click(object sender, RoutedEventArgs e)
        {
            PayrollProcess payrollProcess = new PayrollProcess();
            payrollProcess.Show();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
