using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class ImmediateFixed
    {
        public double CalculateReturns(int age, int retireAge, int deathAge, double mean, double stdDeviation, double amount, TaxStatus taxType, FilingStatus status, double income, List<Riders> Riders)
        {
            double amountWithFees = amount;
            Boolean isDeath;
            if (Riders.Contains(Models.Riders.DeathBenefit))//death benefit rider
            {
                isDeath = true;
                amountWithFees -= amountWithFees * .005;
            }
            else
            {
                isDeath = false;
            }
                double[] account = new double[deathAge-retireAge];
                int count = 0;
                double temp = amountWithFees;
                double principle = amountWithFees;
                for (int j = age; j < deathAge; j++)
                {
                    double rate = mean;
                    if (j == retireAge)
                    {
                        double assetAtRetire = temp;
                    }
                    if (j < retireAge)
                    {
                        temp = temp * Math.Pow(1 + rate, 1);
                    }
                    if (j >= retireAge)
                    {
                        account[count]= CalcWithdrawal(mean, temp, deathAge - j + 1, taxType, status, principle / (deathAge - retireAge));
                        temp -= CalcWithdrawal(mean, temp, deathAge - j+1, taxType, status, principle / (deathAge - retireAge));
                        temp = temp * Math.Pow(1 + rate, 1);
                        count++;
                    }
                }
            double averageWithdrawal = 0;
            for(int i = 0; i < deathAge - retireAge; i++)//calculates average withdrawal during retirement
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
