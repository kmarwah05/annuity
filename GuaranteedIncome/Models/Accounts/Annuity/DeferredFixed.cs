using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class DeferredFixed
    {
        public  double CalculateReturns(int age, int retireAge, int deathAge, double mean, double stdDeviation, double amount, TaxStatus taxType, FilingStatus status, double income,List<Riders> Riders)
        {
            double amountWithFees = amount;
            double principle = 0;
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
            double withdrawalPercentageFee =0;

            /*surrender fee:*/
            if (age+7 < retireAge)
            {
                withdrawalPercentageFee = 0.07;
            }
            else if (age+6 < retireAge)
            {
                withdrawalPercentageFee = 0.06;
            }
            else if (age+5 < retireAge)
            {
                withdrawalPercentageFee = 0.05;
            }
            else if (age+4 < retireAge)
            {
                withdrawalPercentageFee = 0.04;
            }
            else if (age+3 < retireAge)
            {
                withdrawalPercentageFee = 0.03;
            }
            else if (age+2 < retireAge )
            {
                withdrawalPercentageFee = 0.02;
            }
            else if (age+1 < retireAge )
            {
                withdrawalPercentageFee = 0.01;
            }
            /*surender fee:*/


            double[] account = new double[deathAge - retireAge];
                double temp = 0;
                int count = 0;
                for (int j = age; j < deathAge; j++)
                {
                    if (j == retireAge)
                    {
                        double assetAtRetire = temp;
                       
                    }
                    if (j < retireAge)
                    {
                        temp = (temp+amountWithFees) * Math.Pow(1 + mean, 1);
                        principle += amountWithFees;
                    }else
                    {
                    double withdrawal = CalcWithdrawal(mean, temp, deathAge - j + 1, taxType, status, principle / (deathAge - retireAge));
                    withdrawal = withdrawal - withdrawal * withdrawalPercentageFee;
                    account[count] = withdrawal;
                    temp = temp - withdrawal;
                        temp = temp * Math.Pow(1 + mean, 1);
                        count++;
                    }
                   
                }
            double averageWithdrawal = 0;
            for (int i = 0; i < deathAge - retireAge; i++)
            {
                averageWithdrawal += account[i];
            }
            averageWithdrawal = averageWithdrawal / (deathAge - retireAge);

            return averageWithdrawal;
        }
        public double CalcWithdrawal(double rate, double presentValue, int yearsWithdrawing, TaxStatus taxType, FilingStatus status, double principle)
        {
            return TaxHelper.CalcTaxedWithdrawals(rate, presentValue, yearsWithdrawing, taxType, status, principle);
        }

    }
}
