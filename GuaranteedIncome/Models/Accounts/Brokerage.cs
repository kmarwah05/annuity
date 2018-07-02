using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class Brokerage
    {
        public List<double[]> CalculateReturns(int age, int retireAge, int deathAge, double mean, double stdDeviation, double amount,double lumpSum, TaxStatus taxType, FilingStatus status, double income, List<Riders> Riders)
        {
            List<double[]> trials = new List<double[]>();
            double[] account = new double[deathAge+1];
            for (int i = 0; i < 1; i++)
            {
                double temp = lumpSum;
                double returns = 0;
                double taxableAmount = 0;
                double withdrawalSum = 0;
                double principle = lumpSum;
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
                        principle += amount;
                        returns= (temp + amount) * Math.Pow(1 + rate, 1);
                        taxableAmount = returns - temp;
                        temp= returns-Convert.ToDouble(IncomeTaxCalculator.CapitalGainsTaxFor(status, (decimal)taxableAmount, (decimal)income));
                        account[j] = temp;

                    }
                    else
                    {
                        // withdrawalSum += CalcWithdrawal(rate, temp, deathAge - j, taxType, status, amount);
                        temp -= CalcWithdrawal(rate, temp, deathAge - j + 1, taxType, status, principle/(deathAge-retireAge));
                        temp = temp * Math.Pow(1 + rate, 1);
                        account[j] = temp;

                    }
                }
                //trials[i] = withdrawalSum / (deathAge - retireAge);
                account[deathAge] = 0;
                trials.Add(account);
            }
            return trials;
        }
        public double CalcWithdrawal(double rate, double presentValue, int yearsWithdrawing, TaxStatus taxType, FilingStatus status, double principle)
        {
            return TaxHelper.CalcTaxedWithdrawals(rate, presentValue, yearsWithdrawing, taxType, status, principle);
        }


    }
}
