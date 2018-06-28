using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class DeferredFixed:Account
    {
        public override double[] CalculateReturns(int age, int retireAge, int deathAge, double mean, double stdDeviation, double amount, TaxStatus taxType, FilingStatus status, double income)
        {

            double[] trials = new double[100];
            for (int i = 0; i < 100; i++)
            {
                double temp = 0;
                double withdrawalSum = 0;
                for (int j = age; j < deathAge; j++)
                {
                    Random rand = new Random();
                    double rate = mean + stdDeviation * (rand.NextDouble() * (6) - 3);
                    if (j == retireAge)
                    {
                        double assetAtRetire = temp;
                    }
                    if (j < retireAge)
                    {
                        temp = (temp+amount) * Math.Pow(1 + rate, 1);
                    }
                    if (j >= retireAge)
                    {
                        withdrawalSum += CalcWithdrawal(rate, temp, deathAge - j, taxType, status, amount);
                        temp -= CalcWithdrawal(rate, temp, deathAge - j, taxType, status, amount);
                        temp = temp * Math.Pow(1 + rate, 1);
                    }
                }
                trials[i] = withdrawalSum / (deathAge - retireAge);
            }
            return trials;
        }
        public override double CalcWithdrawal(double rate, double presentValue, int yearsWithdrawing, TaxStatus taxType, FilingStatus status, double principle)
        {
            return TaxHelper.CalcWithdrawalAmount(rate, presentValue, yearsWithdrawing) - TaxHelper.CalcTaxedWithdrawals(rate, presentValue, yearsWithdrawing, taxType, status, principle);
        }
    }
}
