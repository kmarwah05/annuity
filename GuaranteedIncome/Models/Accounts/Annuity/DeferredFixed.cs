using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class DeferredFixed:Account
    {
        public  override List<double[]> CalculateReturns(int age, int retireAge, int deathAge, double mean, double stdDeviation, double amount, TaxStatus taxType, FilingStatus status, double income,List<Riders> Riders)
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

            List <double[]> trials= new List<double[]>();
            double[] account = new double[deathAge+1];
            for (int i = 0; i < 1; i++)
            {
                double temp = 0;
                double withdrawalSum = 0;
                for (int j = age; j < deathAge; j++)
                {
                    Random rand = new Random();
                    double rate = mean + stdDeviation * (rand.NextDouble() * (6) - 3);
                    //double rate = mean;
                    if (j == retireAge)
                    {
                        double assetAtRetire = temp;
                       
                    }
                    if (j < retireAge)
                    {
                        temp = (temp+amountWithFees) * Math.Pow(1 + rate, 1);
                        account[j] = temp;
                        principle += amountWithFees;
                    }else
                    {
                        //withdrawalSum += CalcWithdrawal(rate, temp, deathAge - j, taxType, status, amount);
                        temp = temp * Math.Pow(1 + rate, 1);
                        temp = temp- CalcWithdrawal(rate, temp, deathAge-j+1, taxType, status, principle/(deathAge-retireAge));
                        
                        account[j] = temp;
                    }
                }
                //trials[i] = withdrawalSum / (deathAge - retireAge);
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
