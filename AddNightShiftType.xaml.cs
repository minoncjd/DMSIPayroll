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
    /// Interaction logic for AddNightShiftType.xaml
    /// </summary>
    public partial class AddNightShiftType : MetroWindow
    {
        public int mode;
        public int nightshifttypeid;
        public AddNightShiftType()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (mode == 2)
            {
                this.Title = "UPDATE NIGHT SHIFT TYPE";
                GetNightShiftType();
            }
        }

        private void GetNightShiftType()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var nightshiftttype = db.NightShiftTypes.Where(m => m.NightShiftTypeID == nightshifttypeid).FirstOrDefault();
                    tbNightShiftCode.Text = nightshiftttype.NightShiftTypeCode;
                    tbDescription.Text = nightshiftttype.Description;
                    tbMultiplier.Text = nightshiftttype.Multiplier.ToString();
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

                    if (tbDescription.Text == "" || tbMultiplier.Text == "" || tbMultiplier.Text == "")
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (mode == 1)
                    {
                        NightShiftType nightShiftType = new NightShiftType();
                        nightShiftType.NightShiftTypeCode = tbNightShiftCode.Text;
                        nightShiftType.Description = tbDescription.Text;
                        nightShiftType.Multiplier = Decimal.Parse(tbMultiplier.Text);
                        db.NightShiftTypes.Add(nightShiftType);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var nightshiftttype = db.NightShiftTypes.Where(m => m.NightShiftTypeID == nightshifttypeid).FirstOrDefault();
                        nightshiftttype.NightShiftTypeCode = tbNightShiftCode.Text;
                        nightshiftttype.Description = tbDescription.Text;
                        nightshiftttype.Multiplier = Decimal.Parse(tbMultiplier.Text);
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
            tbDescription.Text = "";
            tbMultiplier.Text = "";
            tbNightShiftCode.Text = "";
        }
    }
}
