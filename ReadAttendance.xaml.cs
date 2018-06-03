using DMSIPayroll.Model;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;



namespace DMSIPayroll
{
    /// <summary>
    /// Interaction logic for ReadAttendance.xaml
    /// </summary>
    public partial class ReadAttendance : MetroWindow
    {
        List<DMSIClass.Attendance> lAttendance = new List<DMSIClass.Attendance>();
        public ReadAttendance()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
      
        }

       
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> lString = new List<string>();
                string path = "";
                string line = "";
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    path = openFileDialog.FileName;

                }
                else
                {
                    return;
                }

                if (path != null)
                {
                    var m_readFile = new StreamReader(path);
                    List<string> list = new List<string>();
                    while ((line = m_readFile.ReadLine()) != null)
                    {
                        string[] columns = line.Split(',');

                        foreach (var x in columns)
                        {
                            DMSIClass.Attendance attendance = new DMSIClass.Attendance();
                            var punctuation = x.Where(Char.IsPunctuation).Distinct().ToArray();
                            var test = punctuation.Where(m => !string.IsNullOrEmpty(x.Trim()));
                            var words = x.Split().Select(y => y.Trim(punctuation)).Where(m => !string.IsNullOrEmpty(m.Trim())).ToArray();

                            if (words.Length != 1)
                            {

                                attendance.ID = words[0];
                                attendance.Date = words[1];
                                attendance.Time = words[2];
                                attendance.Mode = words[4];

                                lAttendance.Add(attendance);
                            }


                        }
                    }

                    datagridview.ItemsSource = lAttendance.OrderByDescending(m=>m.Date).ThenByDescending(x=>x.Time).ToList();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
        
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    MessageDialogResult mdr = await this.ShowMessageAsync("READ", "ARE YOU SURE YOU WANT TO SAVE THIS ATTENDANCE?", MessageDialogStyle.AffirmativeAndNegative);

                    if (mdr == MessageDialogResult.Affirmative)
                    {

                        foreach (var x in lAttendance.Where(m=>m.ID == "8166"))
                        {
                            DateTime datetime = DateTime.ParseExact(x.Date + " " + x.Time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                            BiometricsLog bio = new BiometricsLog();
                            bio.BiometricsID = Convert.ToInt32(x.ID);
                            bio.DTime = datetime;
                            bio.Mode = Convert.ToInt32(x.Mode);
                            db.BiometricsLogs.Add(bio);
                            db.SaveChanges();
                        }

                        MessageBox.Show("Saving Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);


                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Return)
        //    {
        //        var search = tbSearch.Text.Trim();
        //        datagridview.ItemsSource = lAttendance.Where(m => m.ID.Contains(search)).OrderBy(m => m.ID);
        //    }
        //}
    }
}
