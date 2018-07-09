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
                double x=Convert.ToDouble(IncomeTaxCalculator.TotalIncomeTaxFor(status, (decimal)yearlyIncome, 0));
                return Convert.ToDouble(IncomeTaxCalculator.TotalIncomeTaxFor(status, (decimal)yearlyIncome, 0));
            }
            else//taxed on only gains, not taxed on principle
            {
                if (yearlyIncome < principle)
                {
                    return 0;
                }
                double y = Convert.ToDouble(IncomeTaxCalculator.TotalIncomeTaxFor(status, (decimal)(yearlyIncome - principle), 0));
                return Convert.ToDouble(IncomeTaxCalculator.TotalIncomeTaxFor(status, (decimal)(yearlyIncome-principle), 0));

            }



        }
        public static double CalcWithdrawalAmount(double rate, double presentValue, int yearsWithdrawing)//loan payment calculator
        {
            
            return rate * presentValue / (1 - Math.Pow((Convert.ToDouble(1 + rate)), -yearsWithdrawing));

        }

        public static double CalcTaxedWithdrawals(double rate, double presentValue,int yearsWithdrawing, TaxStatus taxType, FilingStatus status, double principle)
        {
            //Console.WriteLine(rate+ "     rate    "+presentValue+"    PV    "+CalcTaxes(CalcWithdrawalAmount(rate, presentValue, yearsWithdrawing), taxType, status, principle));
            
            double withdrawal = CalcWithdrawalAmount(rate, presentValue, yearsWithdrawing);
            double taxes =CalcTaxes(withdrawal, taxType, status, principle);
            return withdrawal-taxes;
        }

    }
}
