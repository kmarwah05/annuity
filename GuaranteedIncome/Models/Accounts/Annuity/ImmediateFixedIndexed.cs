using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class ImmediateFixedIndexed
    {
        public double[] CalculateReturns(int age, int retireAge, int deathAge, double mean, double stdDeviation, double amount, TaxStatus taxType, FilingStatus status, double income, List<Riders> Riders)
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
           // List<double[]> trials = new List<double[]>();
            double[] MedianAverageWithdrawal = new double[4000];

            for (int i = 0; i < 4000; i++)
            {
                double[] account = new double[deathAge-retireAge];
                int count = 0;
                double temp = 0;
                temp = amountWithFees;//starting amount is lumpsum
                double principle = amountWithFees;
                double withdrawalAmount = 0;
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
                        withdrawalAmount += account[count];
                        temp -= CalcWithdrawal(mean, temp, deathAge - j+1, taxType, status, principle / (deathAge - retireAge));
                        temp = temp * Math.Pow(1 + rate, 1);

                        count++;
                    }
                }
                //if (i < 500)
                //{
                //    trials.Add(account);//adds one trial to the list
                //}
                withdrawalAmount = withdrawalAmount / (deathAge - retireAge);//calculates average withdrawal
                MedianAverageWithdrawal[i] = withdrawalAmount;//stores the average withdrawal for this trial
            }
            //trials.Add(MedianAverageWithdrawal);//adds an array of the averages to the end of the lsit, will be taken out later and used
            return MedianAverageWithdrawal;
        }

        public double CalcWithdrawal(double rate, double presentValue, int yearsWithdrawing, TaxStatus taxType, FilingStatus status, double principle)
        {
            return TaxHelper.CalcTaxedWithdrawals(rate, presentValue, yearsWithdrawing, taxType, status, principle);
        }
    }
}