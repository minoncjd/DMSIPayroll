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
    /// Interaction logic for AddAdjustmentType.xaml
    /// </summary>
    public partial class AddOvertimeType : MetroWindow
    {
        public int mode;
        public int overtimetypeid;
        public AddOvertimeType()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (mode == 2)
            {
                this.Title = "UPDATE OVERTIME TYPE";
                GetOvertimeType();
            }
        }

        private void GetOvertimeType()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var overtimetype = db.OvertimeTypes.Where(m => m.OvertimeTypeID == overtimetypeid).FirstOrDefault();
                    tbAdjustmentCode.Text = overtimetype.OvertimeTypeCode;
                    tbDescription.Text = overtimetype.Description;
                    tbMultiplier.Text = overtimetype.Multiplier.ToString();
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
                    if (tbAdjustmentCode.Text == "" || tbDescription.Text == "" || tbMultiplier.Text == "")
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;

                    }
                    if (mode == 1)
                    {
                        OvertimeType overtimeType = new OvertimeType();
                        overtimeType.OvertimeTypeCode = tbAdjustmentCode.Text;
                        overtimeType.Description = tbDescription.Text;
                        overtimeType.Multiplier = Decimal.Parse(tbMultiplier.Text);       
                        db.OvertimeTypes.Add(overtimeType);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var overtimeType = db.OvertimeTypes.Where(m => m.OvertimeTypeID == overtimetypeid).FirstOrDefault();
                        overtimeType.OvertimeTypeCode = tbAdjustmentCode.Text;
                        overtimeType.Description = tbDescription.Text;
                        overtimeType.Multiplier = Decimal.Parse(tbMultiplier.Text);
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
            tbMultiplier.Text = "";
            tbDescription.Text = "";
            tbAdjustmentCode.Text = "";
        }
    }
}
