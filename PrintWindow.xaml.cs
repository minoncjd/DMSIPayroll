using CrystalDecisions.CrystalReports.Engine;
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
    /// Interaction logic for PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : MetroWindow
    {
        public int rptid;
        ReportDocument report;
        public string startDate;
        public string endDate;
        public List<GetEmployeeDTR_Result> Report1 = new List<GetEmployeeDTR_Result>();
      
        public PrintWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //crViewer1.Owner = Window.GetWindow(this);
            LoadReport(rptid);
        }

        public void LoadReport(int ReportID)
        {
            try
            {
                using (var db =  new DMSIPayrollEntities())
                {
                    if (ReportID == 1)
                    {
                        if (Report1.Count > 0)
                        {
                            report = new Report.EmployeeDTR();
                            report.SetDataSource(Report1);
                            report.SetDatabaseLogon("sa", "Pa$$w0rd", "DMSIPAYROLL", "DMISPAYROLL");
                            report.SetParameterValue("startDate", startDate);
                            report.SetParameterValue("endDate", endDate);
                            crViewer1.ViewerCore.ReportSource = report;
                        }
                        else
                        {
                            MessageBox.Show("Report cannot be loaded.", "System Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
