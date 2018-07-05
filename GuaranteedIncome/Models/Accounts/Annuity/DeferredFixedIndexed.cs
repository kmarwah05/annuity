using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class DeferredFixedIndexed
    {
        public List<double[]> CalculateReturns(int age, int retireAge, int deathAge, double mean, double stdDeviation, double amount, TaxStatus taxType, FilingStatus status, double income, List<Riders> Riders)
        {
            double withdrawalPercentageFee = 0;
            /*surrender fee:*/
            //fee for withdrawing early
            if (age + 7 < retireAge)
            {
                withdrawalPercentageFee = 0.07;
            }
            else if (age + 6 < retireAge)
            {
                withdrawalPercentageFee = 0.06;
            }
            else if (age + 5 < retireAge)
            {
                withdrawalPercentageFee = 0.05;
            }
            else if (age + 4 < retireAge)
            {
                withdrawalPercentageFee = 0.04;
            }
            else if (age + 3 < retireAge)
            {
                withdrawalPercentageFee = 0.03;
            }
            else if (age + 2 < retireAge)
            {
                withdrawalPercentageFee = 0.02;
            }
            else if (age + 1 < retireAge)
            {
                withdrawalPercentageFee = 0.01;
            }
            /*surender fee:*/

            double amountWithFees = amount;
            Boolean isDeath;
            if (Riders.Contains(Models.Riders.DeathBenefit))//death benefit rider, only increases fee
            {
                isDeath = true;
                amountWithFees -= amountWithFees * .005;
               
            }
            else
            {
                isDeath = false;
            }
            List<double[]> trials = new List<double[]>();
            double[] MedianAverageWithdrawal = new double[5000];
            for (int i = 0; i < 5000; i++)
            {
                double[] account = new double[deathAge-retireAge];
                int count = 0;//index of the array that withdrawal data is being input into
                double temp = 0;//current amount in the annuity 
                double principle = 0;//untaxable part of account
                double withdrawalAmount = 0;

                for (int j = age; j < deathAge; j++)
                {
                    Random rand = new Random();
                    double rate = mean + stdDeviation * (rand.NextDouble() * (6) - 3);//random number from -3 to 3, 3 standard deviations is enough

                    //if rate is less than 1
                    if (rate < .01)//fixed indexed has upper and lower bounds for rate
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
                    if (j < retireAge)//depositing only , no withdrawing
                    {
                        temp = (temp + amountWithFees) * Math.Pow(1 + rate, 1);
                        principle += amountWithFees;//adds to amount invested
                    }
                    if (j >= retireAge)//stop depositing and start withdrawing when retirement starts
                    {
                        double withdrawal = CalcWithdrawal(mean, temp, deathAge - j + 1, taxType, status, principle / (deathAge - retireAge));//withdrawal amoutn with taxes and loan payment calc
                        withdrawal = withdrawal - withdrawal * withdrawalPercentageFee;// subtract withdrawal early fee
                        account[count] = withdrawal;
                        withdrawalAmount += withdrawal;
                        temp -= withdrawal;
                        temp = temp * Math.Pow(1 + rate, 1);//interest
                        count++;//increment array counter
                    }
                }
                if (i < 100)
                {
                    trials.Add(account);//adds one trial to the list
                }
                withdrawalAmount = withdrawalAmount / (deathAge - retireAge);//calculates average withdrawal
                MedianAverageWithdrawal[i] = withdrawalAmount;//stores the average withdrawal for this trial
            }
            trials.Add(MedianAverageWithdrawal);//adds an array of the averages to the end of the lsit, will be taken out later and used
            return trials;
        }
        public double CalcWithdrawal(double rate, double presentValue, int yearsWithdrawing, TaxStatus taxType, FilingStatus status, double principle)
        {
            return  TaxHelper.CalcTaxedWithdrawals(rate, presentValue, yearsWithdrawing, taxType, status, principle);
        }
    }
}
