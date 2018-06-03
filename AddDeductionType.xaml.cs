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
    /// Interaction logic for AddDeductionType.xaml
    /// </summary>
    public partial class AddDeductionType : MetroWindow
    {
        public int mode;
        public int deductiontypeid;

        public AddDeductionType()
        {
            InitializeComponent();
        }

        private void GetIncomeType()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var deductiontype = db.DeductionTypes.Where(m => m.DeductionTypeID == deductiontypeid).FirstOrDefault();
                    deductiontype.Description = tbDescription.Text;
                    deductiontype.DeductionCode = tbDeductionCode.Text;
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
                this.Title = "UPDATE DEDCUTION TYPE";
                GetIncomeType();
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    if (tbDeductionCode.Text == "" || tbDescription.Text == "")
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (mode == 1)
                    {
                        DeductionType deductionType = new DeductionType();
                        deductionType.DeductionCode = tbDeductionCode.Text;
                        deductionType.Description = tbDescription.Text;
                        db.DeductionTypes.Add(deductionType);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var deductiontype = db.DeductionTypes.Where(m => m.DeductionTypeID == deductiontypeid).FirstOrDefault();
                        deductiontype.DeductionCode = tbDeductionCode.Text;
                        deductiontype.Description = tbDescription.Text;              
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
            tbDeductionCode.Text = "";
            tbDescription.Text = "";
            
        }
    }
}
