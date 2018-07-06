using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class DeferredFixed
    {
        public double CalculateReturns(int age, int retireAge, int deathAge, double mean, double stdDeviation, double amount, TaxStatus taxType, FilingStatus status, double income, List<Riders> Riders)
        {
            double amountWithFees = amount;
            Boolean isDeath;
            if (Riders.Contains(Models.Riders.DeathBenefit))//rider for Death benefit, doesn't actually do anything except add to the fees
            {
                isDeath = true;
                amountWithFees -= amountWithFees * .005;
            }
            else
            {
                isDeath = false;
            }

            double withdrawalPercentageFee = 0;

            /*surrender fee:*/
            //fee for withdrawaling funds too early
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

            double principle = 0;

            double[] account = new double[deathAge - retireAge];
            double temp = 0;//the amount in the account at year "j"
            int count = 0;//counter for filling up array that is returned
            for (int j = age; j < deathAge; j++)
            {
                if (j == retireAge)
                {
                    double assetAtRetire = temp;

                }
                if (j < retireAge)//when less than retire age, continue to deposit and no withdrawl
                {
                    temp = (temp + amountWithFees) * Math.Pow(1 + mean, 1);
                    principle += amountWithFees;
                }
                else//when greater than retire age, stop depositing and start withdrawing
                {
                    double withdrawal = CalcWithdrawal(mean, temp, deathAge - j + 1, taxType, status, principle / (deathAge - retireAge));//withdrawal amoutn using payment calculator
                    withdrawal = withdrawal - withdrawal * withdrawalPercentageFee;//withdrawal fee for doing it early
                    account[count] = withdrawal;
                    temp = temp - withdrawal;
                    temp = temp * Math.Pow(1 + mean, 1);//interest 
                    count++;
                }

            }
            double averageWithdrawal = 0;//just create average withdrawal amount for fixed
            for (int i = 0; i < deathAge - retireAge; i++)
            {
                averageWithdrawal += account[i];
            }
            averageWithdrawal = averageWithdrawal / (deathAge - retireAge);

            return averageWithdrawal;
        }
        public double CalcWithdrawal(double rate, double presentValue, int yearsWithdrawing, TaxStatus taxType, FilingStatus status, double principle)//tax helper method
        {
            return TaxHelper.CalcTaxedWithdrawals(rate, presentValue, yearsWithdrawing, taxType, status, principle);
        }

    }
}
