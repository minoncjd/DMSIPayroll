using DMSIPayroll.Model;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
    /// Interaction logic for PayrollProcess.xaml
    /// </summary>
    public partial class PayrollProcess : MetroWindow
    {
        List<DMSIClass.PayrollDetails> lPayrollDetails = new List<DMSIClass.PayrollDetails>();
        List<Income> income = new List<Income>();
        List<Tardy> tardy = new List<Tardy>();
        List<Holiday> holiday = new List<Holiday>();
        List<Loan> loan = new List<Loan>();
        List<Deduction> deduction = new List<Deduction>();
        List<Overtime> overtime = new List<Overtime>();
        List<NightDifferential> nightdiff = new List<NightDifferential>();
        List<Adjustment> adjustment = new List<Adjustment>();
        List<OtherIncome> otherinc = new List<OtherIncome>();
        List<Leave> leave = new List<Leave>();
        public PayrollProcess()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    cbPeriod.ItemsSource = db.Periods.Where(m=>m.PeriodID != 3).OrderBy(m => m.PeriodDescription).ToList();
                    cbPeriod.DisplayMemberPath = "PeriodDescription";
                    cbPeriod.SelectedValuePath = "PeriodID";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
        }

        private void viewMasterForm_Click(object sender, RoutedEventArgs e)
        {
            var x = ((DMSIClass.PayrollDetails)datagridview.SelectedItem);
            EmployeeMaster employeeMaster = new EmployeeMaster();
            employeeMaster.empid = x.EmployeeID;     
            employeeMaster.Show();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    if (dpStDate.SelectedDate == null || dpToDate.SelectedDate == null)
                    {
                        MessageBox.Show("Fill up the required fields.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var from = dpStDate.SelectedDate;
                    var to = dpToDate.SelectedDate;
                    var periodid = Convert.ToInt32(cbPeriod.SelectedValue);
                    var employees = db.Employees.Where(m=>m.PayrollType == 2).OrderBy(m => m.LastName).ToList();
                    lPayrollDetails = new List<DMSIClass.PayrollDetails>();


                    foreach (var x in employees)
                    {
                        DMSIClass.PayrollDetails payroll = new DMSIClass.PayrollDetails();

                        income = db.Incomes.Where(m => m.EmployeeID == x.EmployeeID && from <= m.PayrollDate && to >= m.PayrollDate && m.PayrollID == null).ToList();
                        var position = db.EmployeePositions.Where(m => m.EmployeePositionID == x.EmployeePositionID).FirstOrDefault();                     
                        tardy = db.Tardies.Where(m => m.EmployeeID == x.EmployeeID && from <= m.PayrollDate && to >= m.PayrollDate && m.PayrollID == null).ToList();
                        holiday = db.Holidays.Where(m => m.EmployeeID == x.EmployeeID && from <= m.PayrollDate && to >= m.PayrollDate && m.PayrollID == null).ToList();
                        overtime = db.Overtimes.Where(m => m.EmployeID == x.EmployeeID && from <= m.PayrollDate && to >= m.PayrollDate && m.PayrollID == null).ToList();
                        nightdiff = db.NightDifferentials.Where(m => m.EmployeeID == x.EmployeeID && from <= m.PayrollDate && to >= m.PayrollDate && m.PayrollID == null).ToList();
                        adjustment = db.Adjustments.Where(m => m.EmployeeID == x.EmployeeID && from <= m.PayrollDate && to >= m.PayrollDate && m.PayrollID == null).ToList();
                        leave = db.Leaves.Where(m => m.EmployeeID == x.EmployeeID && from <= m.PayrollDate && to >= m.PayrollDate && m.PayrollID == null).ToList();

                        if (periodid == 1 || periodid == 2)
                        {
                            loan = db.Loans.Where(m => m.EmployeeID == x.EmployeeID && from >= m.StDate && to <= m.ToDate && (m.PeriodID == periodid || m.PeriodID == 3)).ToList();
                            deduction = db.Deductions.Where(m => m.EmployeeID == x.EmployeeID && from >= m.StDate && to <= m.ToDate && (m.PeriodID == periodid || m.PeriodID == 3)).ToList();
                            otherinc = db.OtherIncomes.Where(m => m.EmployeeID == x.EmployeeID && from >= m.StDate && to <= m.ToDate && (m.PeriodID == periodid || m.PeriodID == 3)).ToList();
                        }

                        payroll.EmployeeID = x.EmployeeID;
                        payroll.EmployeeNumber = x.EmployeeNumber;
                        payroll.Name = (x.LastName + ", " + x.FirstName + " " + x.MiddleName).ToUpper();
                        payroll.Position = position.PositionName;
                        payroll.BasicRate = position == null ? 0 : position.DailyRate;
                        payroll.BasicNoOfDays = income == null ? 0 : income.Sum(m => m.NoOfDays);
                        payroll.BasicAmount = payroll.BasicRate * payroll.BasicNoOfDays;
                        payroll.NightDiff = nightdiff == null ? 0 : nightdiff.Sum(m => m.Amount);
                        payroll.OverTime = overtime == null ? 0 : overtime.Sum(m => m.Amount);
                        payroll.HolidayNoOfDays = holiday == null ? 0 : holiday.Sum(m => m.NoOfDays);
                        payroll.HolidayAmount = holiday == null ? 0 : holiday.Sum(m => m.Amount);
                        payroll.LateUndertimeAmount = tardy == null ? 0 : tardy.Sum(M => M.Amount);
                        payroll.LateUndertimeNoOfMins = tardy == null ? 0 : tardy.Sum(m => m.Value);
                        payroll.Adjustment = adjustment == null ? 0 : adjustment.Sum(m => m.Amount);
                        payroll.OtherInc = otherinc == null ? 0 : otherinc.Sum(m => m.Amount);
                        payroll.Gross = (payroll.BasicAmount + payroll.HolidayAmount + payroll.OverTime + payroll.NightDiff) - payroll.LateUndertimeAmount;


                        payroll.Deduction = deduction == null ? 0 : deduction.Sum(m => m.Amortization);
                        payroll.Loan = loan == null ? 0 : loan.Sum(m => m.Amortization);
                        payroll.Leave = leave == null ? 0 : leave.Sum(m => m.Amount);
                        var sss = db.PYSSSes.Where(m => m.MinSalary <= payroll.Gross && m.MaxSalary >= payroll.Gross).FirstOrDefault();
                        var philhealth = db.Philhealths.Where(m => m.MinSalary <= payroll.Gross && m.MaxSalary >= payroll.Gross).FirstOrDefault();

                        if (philhealth != null)
                        {
                            if (philhealth.PHID == 2)
                            {
                                payroll.Philhealth = ((payroll.Gross * (decimal)2.75) / 100) / 2;
                            }
                            else if (payroll.Gross <= 10000 && payroll.Gross != 0 || payroll.Gross >= 40000 && payroll.Gross != 0)
                            {
                                payroll.Philhealth = philhealth.Ee;
                            }
                        }
                        


                        if (payroll.Gross <= 1500)
                        {
                            payroll.Pagibig = payroll.Gross * (decimal)0.01;
                        }
                        else
                        {
                            payroll.Pagibig = payroll.Gross * (decimal)0.02;
                        }

                        payroll.SSS = sss == null ? 0 : sss.Ee;

                        payroll.Net = (payroll.Gross)  - (payroll.SSS + payroll.Pagibig + payroll.Philhealth + payroll.Loan + payroll.Deduction);
                        lPayrollDetails.Add(payroll);

                    }

                    datagridview.ItemsSource = lPayrollDetails.ToList();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private async void btnPost_Click(object sender, RoutedEventArgs e)
        {
            MessageDialogResult mdr = await this.ShowMessageAsync("POST", "ARE YOU SURE YOU WANT TO POST THIS PAYROLL?", MessageDialogStyle.AffirmativeAndNegative);

            if (mdr == MessageDialogResult.Affirmative)
            {
                PayrollPost payrollPost = new PayrollPost();
                payrollPost.StDate = dpStDate.SelectedDate.Value;
                payrollPost.Todate = dpToDate.SelectedDate.Value;
                payrollPost.lPayrollDetails = lPayrollDetails;
                payrollPost.income = income;
                payrollPost.tardy = tardy;
                payrollPost.holiday = holiday;
                payrollPost.loan = loan;
                payrollPost.deduction = deduction;
                payrollPost.overtime = overtime;
                payrollPost.nightdiff = nightdiff;
                payrollPost.ShowDialog();
            }
        }
    }
}
