using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class ImmediateFixedIndexed: Account
    {
        public override List<double[]> CalculateReturns(int age, int retireAge, int deathAge, double mean, double stdDeviation, double amount, TaxStatus taxType, FilingStatus status, double income, List<Riders> Riders)
        {
            double amountWithFees = amount;
            Boolean isDeath;
            if (Riders.Contains(Models.Riders.DeathBenefit))
            {
                isDeath = true;
                amountWithFees -= amountWithFees * .005;
            }
            else
            {
                isDeath = false;
            }
            List<double[]> trials = new List<double[]>();
            double[] account = new double[deathAge+1];
            for (int i = 0; i < 1; i++)
            {
                double temp = 0;
                temp = amountWithFees;
                double withdrawalSum = 0;
                double principle = amountWithFees;
                for (int j = age; j < deathAge; j++)
                {
                    Random rand = new Random();
                    double rate = mean + stdDeviation * (rand.NextDouble() * (6) - 3);
                    //if rate is less than 1 
                    if (rate < .01)
                    {
                        rate = .01;
                    }
                    else if (rate > .06)
                    {
                        rate = .06;
                    }

                    if (j == retireAge)
                    {
                        double assetAtRetire = temp;
                    }
                    if (j < retireAge)
                    {
                        temp = temp * Math.Pow(1 + rate, 1);
                        account[j] = temp;
                    }
                    else
                    {
                        // withdrawalSum += CalcWithdrawal(rate, temp, deathAge - j, taxType, status, amount);
                        temp -= CalcWithdrawal(rate, temp, deathAge - j+1, taxType, status, principle / (deathAge - retireAge));
                        temp = temp * Math.Pow(1 + rate, 1);
                        account[j] = temp;
                    }
                }
                //  trials[i] = withdrawalSum / (deathAge - retireAge);
                account[deathAge] = 0;

                trials.Add(account);
            }
            return trials;
        }

        public override double CalcWithdrawal(double rate, double presentValue, int yearsWithdrawing, TaxStatus taxType, FilingStatus status, double principle)
        {
            return TaxHelper.CalcTaxedWithdrawals(rate, presentValue, yearsWithdrawing, taxType, status, principle);
        }
    }
}