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
           
            for (int i = 0; i < 500; i++)
            {
                double[] account = new double[deathAge-retireAge];
                int count = 0;
                double temp = 0;
                temp = amountWithFees;//starting amount is lumpsum
                double principle = amountWithFees;
                for (int j = age; j < deathAge; j++)
                {
                    Random rand = new Random();
                    double rate = mean + stdDeviation * (rand.NextDouble() * (6) - 3);
                    //if rate is less than 1 
                    if (rate < .01)//lower bound and upper boudn on interest rate for fixed indexed
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
                    if (j < retireAge)//interest grows on initial investment
                    {
                        temp = temp * Math.Pow(1 + rate, 1);
                    }
                    else
                    {
                        account[count]= CalcWithdrawal(mean, temp, deathAge - j + 1, taxType, status, principle / (deathAge - retireAge));
                        temp -= CalcWithdrawal(mean, temp, deathAge - j+1, taxType, status, principle / (deathAge - retireAge));
                        temp = temp * Math.Pow(1 + rate, 1);
                        count++;
                    }
                }
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