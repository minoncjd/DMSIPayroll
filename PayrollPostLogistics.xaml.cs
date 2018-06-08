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
    /// Interaction logic for PayrollPostLogistics.xaml
    /// </summary>
    public partial class PayrollPostLogistics : MetroWindow
    {
        public DateTime StDate;
        public DateTime Todate;
        public List<Income> income = new List<Income>();
        public List<Tardy> tardy = new List<Tardy>();
        public List<Holiday> holiday = new List<Holiday>();
        public List<Loan> loan = new List<Loan>();
        public List<Deduction> deduction = new List<Deduction>();
        public List<Overtime> overtime = new List<Overtime>();
        public List<NightDifferential> nightdiff = new List<NightDifferential>();
        public List<Adjustment> adjustment = new List<Adjustment>();
        public List<OtherIncome> otherinc = new List<OtherIncome>();
        public List<Leave> leave = new List<Leave>();
        public List<DMSIClass.PayrollDetails> lPayrollDetails = new List<DMSIClass.PayrollDetails>();

        public PayrollPostLogistics()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DMSIPayrollEntities())
                {
                    if (string.IsNullOrEmpty(tbPayrollCode.Text) || dpPayrollDate.SelectedDate == null)
                    {
                        MessageBox.Show("Fill up the required fields.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    PYTable pyTable = new PYTable();
                    foreach (var x in lPayrollDetails)
                    {
                        PayrollLogistic payroll = new PayrollLogistic();

                        pyTable.PayrollLogistics.Add(new PayrollLogistic()
                        {

                            EmployeeID = x.EmployeeID,
                            RP1 = Convert.ToDecimal(x.RP1Amount == null ? 0 : x.RP1Amount),
                            RP2 = Convert.ToDecimal(x.RP2Amount == null ? 0 : x.RP2Amount),
                            NightDifferential = Convert.ToDecimal(x.NightDiffamount),
                            Overtime = Convert.ToDecimal(x.OTAmount),
                            Holiday = Convert.ToDecimal(x.HolidayAmount),
                            Tardiness = Convert.ToDecimal(x.LateUndertimeAmount),
                            Adjustment = Convert.ToDecimal(x.Adjustment),
                            OtherIncome = Convert.ToDecimal(x.OtherInc),
                            Gross = Convert.ToDecimal(x.Gross),
                            SSS = Convert.ToDecimal(x.SSS),
                            PAGIBIG = Convert.ToDecimal(x.Pagibig),
                            PHILHEALTH = Convert.ToDecimal(x.Philhealth),
                            Loan = Convert.ToDecimal(x.Loan),
                            Deduction = Convert.ToDecimal(x.Deduction),
                            Leaves = Convert.ToDecimal(x.Leave),
                        });
                    }

                    pyTable.PYCode = tbPayrollCode.Text;
                    pyTable.StDate = StDate;
                    pyTable.ToDate = Todate;
                    pyTable.PYDate = dpPayrollDate.SelectedDate.Value;
                    pyTable.Comments = tbComment.Text;
                    pyTable.PYType = 1;

                    db.PYTables.Add(pyTable);
                    db.SaveChanges();

                    var pytable = db.PYTables.OrderByDescending(m => m.PYTableID).Take(1).FirstOrDefault();
                    var payrolls = db.PayrollLogistics.Where(m => m.PYTableID == pytable.PYTableID).ToList();

                    foreach (var x in payrolls)
                    {
                        if (income != null)
                        {
                            foreach (var y in income)
                            {
                                var income = db.Incomes.Where(m => m.IncomeID == y.IncomeID).FirstOrDefault();
                                income.PayrollID = x.PayrollID;

                            }

                            foreach (var y in holiday)
                            {
                                var holiday = db.Holidays.Where(m => m.HolidayID == y.HolidayID).FirstOrDefault();
                                holiday.PayrollID = x.PayrollID;

                            }

                            foreach (var y in tardy)
                            {
                                var tardy = db.Tardies.Where(m => m.TardyID == y.TardyID).FirstOrDefault();
                                tardy.PayrollID = x.PayrollID;

                            }

                            foreach (var y in overtime)
                            {
                                var tardy = db.Overtimes.Where(m => m.OvertimeID == y.OvertimeID).FirstOrDefault();
                                tardy.PayrollID = x.PayrollID;

                            }

                            foreach (var y in nightdiff)
                            {
                                var tardy = db.NightDifferentials.Where(m => m.NightDifferentialID == y.NightDifferentialID).FirstOrDefault();
                                tardy.PayrollID = x.PayrollID;

                            }


                            foreach (var y in leave)
                            {
                                var leave = db.Leaves.Where(m => m.LeaveID == y.LeaveID).FirstOrDefault();
                                leave.PayrollID = x.PayrollID;

                            }


                            foreach (var y in adjustment)
                            {
                                var adjustment = db.Adjustments.Where(m => m.AdjustmentID == y.AdjustmentID).FirstOrDefault();
                                adjustment.PayrollID = x.PayrollID;
                                db.SaveChanges();
                            }


                            foreach (var y in otherinc)
                            {
                                PayrollDetails_OtherIncome otherincomeDetails = new PayrollDetails_OtherIncome();
                                otherincomeDetails.PayrollID = x.PayrollID;
                                otherincomeDetails.OtherIncomeID = y.OtherIncomeID;
                                otherincomeDetails.Amount = y.Amount;
                                db.PayrollDetails_OtherIncome.Add(otherincomeDetails);

                            }

                            foreach (var y in deduction)
                            {
                                PayrollDetails_Deduction deductionDetails = new PayrollDetails_Deduction();
                                deductionDetails.PayrollID = x.PayrollID;
                                deductionDetails.DeductionID = y.DeductionID;
                                deductionDetails.Amount = y.Amortization;
                                db.PayrollDetails_Deduction.Add(deductionDetails);


                            }

                            foreach (var y in loan)
                            {
                                PayrollDetails_Loan loanDetails = new PayrollDetails_Loan();
                                loanDetails.PayrollID = x.PayrollID;
                                loanDetails.LoanID = y.LoanID;
                                loanDetails.Amount = y.Amortization;
                                db.PayrollDetails_Loan.Add(loanDetails);

                            }


                        }
                    }
                    db.SaveChanges();
                    MessageBox.Show("Posting payroll success.", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    clear();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void clear()
        {
            dpPayrollDate.SelectedDate = null;
            tbComment.Text = "";
            tbPayrollCode.Text = "";

        }
    }
}
