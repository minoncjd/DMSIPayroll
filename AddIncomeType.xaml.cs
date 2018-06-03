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
    /// Interaction logic for AddIncomeType.xaml
    /// </summary>
    public partial class AddIncomeType : MetroWindow
    {
        public int mode;
        public int incometypeid;

        public AddIncomeType()
        {
            InitializeComponent();
        }
        
        private void GetIncomeType()
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    var incomeType = db.IncomeTypes.Where(m => m.IncomeTypeID == incometypeid).FirstOrDefault();
                    tbDescription.Text = incomeType.Description;
                    tbIncomeTypeCode.Text = incomeType.IncomeTypeCode;
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
                this.Title = "UPDATE INCOME TYPE";
                GetIncomeType();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    if (tbDescription.Text == "" || tbIncomeTypeCode.Text == "")
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (mode == 1)
                    {
                        IncomeType incomeType = new IncomeType();
                        incomeType.IncomeTypeCode = tbIncomeTypeCode.Text;
                        incomeType.Description = tbDescription.Text;
                        db.IncomeTypes.Add(incomeType);
                        db.SaveChanges();
                        MessageBox.Show("Add Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else if (mode == 2)
                    {
                        var incomeType = db.IncomeTypes.Where(m => m.IncomeTypeID == incometypeid).FirstOrDefault();
                        incomeType.IncomeTypeCode = tbIncomeTypeCode.Text;
                        incomeType.Description = tbDescription.Text;                      
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
            tbIncomeTypeCode.Text = "";
        }
    }
}
