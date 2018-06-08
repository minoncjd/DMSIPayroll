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
    /// Interaction logic for PayrollList.xaml
    /// </summary>
    public partial class PayrollTableList : MetroWindow
    {
        List<DMSIClass.PYTable> lPYTable = new List<DMSIClass.PYTable>();
        public int pytype;
        
        public PayrollTableList()
        {
            InitializeComponent();
        }

        private void GetPYTable()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    lPYTable = new List<DMSIClass.PYTable>();
                    var pytables = db.PYTables.Where(m=>m.PYType == pytype).OrderBy(m=>m.PYTableID).ToList();

                    foreach (var x in pytables)
                    {
                        DMSIClass.PYTable pytable = new DMSIClass.PYTable();
                        pytable.Comments = x.Comments;
                        pytable.Duration = x.StDate.ToShortDateString() + " - " + x.ToDate.ToShortDateString();
                        pytable.PYCode = x.PYCode;
                        pytable.PYDate = x.PYDate;
                        pytable.PYTableID = x.PYTableID;
                        lPYTable.Add(pytable);
                    }

                    datagridview.ItemsSource = lPYTable.OrderBy(m => m.PYTableID).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetPYTable();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetPYTable();
        }

        private void viewPayroll_Click(object sender, RoutedEventArgs e)
        {
            var x = ((DMSIClass.PYTable)datagridview.SelectedItem);
            PayrollList payrollList = new PayrollList();
            payrollList.pytableid = x.PYTableID;
            payrollList.Show();

        }

        private void printBilling_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
