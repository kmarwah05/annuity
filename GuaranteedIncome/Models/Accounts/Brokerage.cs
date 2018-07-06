using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class Brokerage
    {
        public double[] CalculateReturns(int age, int retireAge, int deathAge, double mean, double stdDeviation, double amount,double lumpSum, TaxStatus taxType, FilingStatus status, double income, List<Riders> Riders)
        {
            double[] MedianAverageWithdrawal = new double[4000];
            for (int i = 0; i < 4000; i++)
            {
                double temp = lumpSum;//sets temp to equal lumpsum, if deferred this is 0
                double returns = 0;
                double taxableAmount = 0;
                double principle = lumpSum;
                double withdrawalAmount = 0;
                for (int j = age; j < deathAge; j++)
                {
                    Random rand = new Random();
                    double rate = mean + stdDeviation * (rand.NextDouble() * (6) - 3);//random number between -3 and 3, then multiplied by std deviation

                    if (j == retireAge)
                    {
                        double assetAtRetire = temp;
                    }
                    if (j < retireAge)
                    {
                        principle += amount;
                        returns= (temp + amount) * Math.Pow(1 + rate, 1);//total returns
                        taxableAmount = returns - temp;//interest made of of returns
                        temp = returns;  //-Convert.ToDouble(IncomeTaxCalculator.CapitalGainsTaxFor(status, (decimal)taxableAmount, (decimal)income));//capitol gains tax subtracted from interest made
                    }
                    else
                    {
                        withdrawalAmount += CalcWithdrawal(mean, temp, deathAge - j + 1, taxType, status, principle / (deathAge - retireAge));
                        temp -= CalcWithdrawal(mean, temp, deathAge - j + 1, taxType, status, principle/(deathAge-retireAge));
                        temp = temp * Math.Pow(1 + rate, 1);
                    }
                }
                
                withdrawalAmount = withdrawalAmount / (deathAge - retireAge);//calculates average withdrawal
                MedianAverageWithdrawal[i] = withdrawalAmount;//stores the average withdrawal for this trial
            }
            return MedianAverageWithdrawal;
        }
        public double CalcWithdrawal(double rate, double presentValue, int yearsWithdrawing, TaxStatus taxType, FilingStatus status, double principle)
        {
            return TaxHelper.CalcTaxedWithdrawals(rate, presentValue, yearsWithdrawing, taxType, status, principle);
        }


    }
}
