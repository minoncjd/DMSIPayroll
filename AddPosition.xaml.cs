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
    /// Interaction logic for AddPosition.xaml
    /// </summary>
    public partial class AddPosition : MetroWindow
    {
        public int mode;
        public int positionid;
        public AddPosition()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (mode == 2)
            {
                this.Title = "UPDATE POSITION";
                GetPosition();
            }
        }


        private void GetPosition()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var position = db.EmployeePositions.Where(m => m.EmployeePositionID == positionid).FirstOrDefault();
                    tbPosition.Text = position.PositionName;
                    tbDailyRate.Text = position.DailyRate.ToString();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {

                    if (tbDailyRate.Text == "" || tbPosition.Text == "")
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (mode == 1)
                    {
                        EmployeePosition position = new EmployeePosition();
                        position.PositionName = tbPosition.Text;
                        position.DailyRate = Decimal.Parse(tbDailyRate.Text);
                        db.EmployeePositions.Add(position);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var position = db.EmployeePositions.Where(m => m.EmployeePositionID == positionid).FirstOrDefault();
                        position.PositionName = tbPosition.Text;
                        position.DailyRate = Decimal.Parse(tbDailyRate.Text);
                        db.SaveChanges();
                        MessageBox.Show("Update Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
            }

            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void clear()
        {
            tbDailyRate.Text = "";
            tbPosition.Text = "";
        }
    }
}
