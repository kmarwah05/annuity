using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public static class TaxHelper
    {

        public static double CalcTaxes(double yearlyIncome,TaxStatus taxType, FilingStatus status,double principle)
        {
            
            if (TaxStatus.roth==taxType)//no taxes taken out
            {
                return 0;
            }
            else if(TaxStatus.qualified == taxType)//taxed on all withdrawals
            {
                return Convert.ToDouble(IncomeTaxCalculator.TotalIncomeTaxFor(status, (decimal)yearlyIncome, 0));
            }
            else//taxed on only gains, not taxed on principle
            {
                return Convert.ToDouble(IncomeTaxCalculator.TotalIncomeTaxFor(status, (decimal)(yearlyIncome-principle), 0));

            }



        }
        public static double CalcWithdrawalAmount(double rate, double presentValue, int yearsWithdrawing)//loan payment calculator
        {
            
            return rate * presentValue / (1 - Math.Pow((Convert.ToDouble(1 + rate)), -yearsWithdrawing));

        }

        public static double CalcTaxedWithdrawals(double rate, double presentValue,int yearsWithdrawing, TaxStatus taxType, FilingStatus status, double principle)
        {
            return CalcWithdrawalAmount(rate, presentValue, yearsWithdrawing) - CalcTaxes(CalcWithdrawalAmount(rate, presentValue,yearsWithdrawing), taxType,status,principle);
        }

    }
}
