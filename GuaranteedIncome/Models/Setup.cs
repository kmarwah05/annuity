using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class Setup
    {
        private int CurrentAge { get; set; }
        private int RetireAge { get; set; }
        private int DeathAge { get; set; }
        private Gender Gender { get; set; }
        private FilingStatus FilingStatus { get; set; }
        private decimal InitialAmount { get; set; }
        private decimal YearlyAdditions { get; set; }
        private TaxStatus TaxType { get; set; }
        private int YearsWithdrawing { get; set; }
        private decimal Principle { get; set; }


        public Setup(FormModel myModel)
        {
            CurrentAge = myModel.CurrentAge;
            RetireAge = myModel.RetireAge;
            //DeathAge=calculateDeathAge();
            //Gender = myModel.Gender;
            FilingStatus = myModel.FilingStatus;
            InitialAmount = myModel.InitialAmount;
            YearlyAdditions = myModel.YearlyAdditioins;
            TaxType = myModel.TaxType;

            if (myModel.WithdrawalUntil == 0)
            {
                YearsWithdrawing = DeathAge-RetireAge;
            }
            else
            {
                YearsWithdrawing = myModel.WithdrawalUntil;
            }
        }


        public decimal CalcTaxes(decimal yearlyIncome)
        {
            
            if (TaxStatus.roth==TaxType)//no taxes taken out
            {
                return yearlyIncome;
            }
            else if(TaxStatus.qualified == TaxType)//taxed on all withdrawals
            {
                return IncomeTaxCalculator.TotalIncomeTaxFor(FilingStatus, yearlyIncome, 0);

            }
            else//taxed on only gains, not taxed on principle
            {
                return IncomeTaxCalculator.TotalIncomeTaxFor(FilingStatus, (yearlyIncome-Principle), 0);

            }



        }
        public decimal CalcWithdrawalAmount(decimal rate, decimal presentValue)//loan payment calculator
        {
            return rate * presentValue / (decimal)(1 - Math.Pow((Convert.ToDouble(1 + rate)), -YearsWithdrawing));

        }

        public decimal CalcTaxedWithdrawals(decimal rate, decimal presentValue)
        {
            return CalcWithdrawalAmount(rate, presentValue) - CalcTaxes(CalcWithdrawalAmount(rate, presentValue));
        }

    }
}
