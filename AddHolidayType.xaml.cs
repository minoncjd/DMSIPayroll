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
    /// Interaction logic for AddHolidayType.xaml
    /// </summary>
    public partial class AddHolidayType : MetroWindow
    {
        public int mode;
        public int holidaytypeid;

        public AddHolidayType()
        {
            InitializeComponent();
        }

        private void GetHolidayType()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var holidaytype = db.HolidayTypes.Where(m => m.HolidayTypeID == holidaytypeid).FirstOrDefault();
                    tbDescription.Text = holidaytype.Description;
                    tbIncomeTypeCode.Text = holidaytype.HolidayCode ;
                    tbMultiplier.Text = holidaytype.Multiplier.ToString();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (mode == 2)
            {
                this.Title = "UPDATE HOLIDAY TYPE";
                GetHolidayType();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                using (var db = new DMSIPayrollEntities())
                {
                    if (tbIncomeTypeCode.Text == "" || tbMultiplier.Text == "" || tbDescription.Text == "")
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (mode == 1)
                    {
                        HolidayType holidayType = new HolidayType();
                        holidayType.HolidayCode = tbIncomeTypeCode.Text;
                        holidayType.Description = tbDescription.Text;
                        holidayType.Multiplier = Decimal.Parse(tbMultiplier.Text);
                        db.HolidayTypes.Add(holidayType);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var holidayType = db.HolidayTypes.Where(m => m.HolidayTypeID == holidaytypeid).FirstOrDefault();
                        holidayType.HolidayCode = tbIncomeTypeCode.Text;
                        holidayType.Description = tbDescription.Text;
                        holidayType.Multiplier = Decimal.Parse(tbMultiplier.Text);
                        db.SaveChanges();
                        MessageBox.Show("Update Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);

                    }

                }
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            //}

        }

        private void clear()
        {
            tbDescription.Text = "";
            tbIncomeTypeCode.Text = "";
            tbMultiplier.Text = "";
        }
    }
}
