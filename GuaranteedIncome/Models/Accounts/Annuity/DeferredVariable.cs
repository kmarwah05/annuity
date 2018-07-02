using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class DeferredVariable:Account
    {
        public override List<double[]> CalculateReturns(int age, int retireAge, int deathAge, double mean, double stdDeviation, double amount, TaxStatus taxType, FilingStatus status, double income,List<Riders> Riders)
        {
           

            double amountWithFees = amount;
            double principle = 0;
            Boolean isGMWB;
            if (Riders.Contains(Models.Riders.GMWB))//Checks to see if GMWB is a rider
            {
                isGMWB = true;
                amountWithFees -= amountWithFees * .005;
            }
            else
            {
                isGMWB = false;
            }

            Boolean isGMAB;
            if (Riders.Contains(Models.Riders.GMAB))
            {
                isGMAB = true;
                amountWithFees -= amountWithFees * .005;
            }
            else
            {
                isGMAB = false;
            }

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
                double withdrawalSum = 0;
                double minWithdrawal = 0;
                for (int j = age; j < deathAge; j++)
                {
                    Random rand = new Random();
                    double rate = mean + stdDeviation * (rand.NextDouble() * (6) - 3);
                    if (j == retireAge)
                    {
                        double assetAtRetire = temp;
                        if (assetAtRetire < principle && isGMAB)//if they have GMAB then set Amount in Annuity equal to principle. (only if amount is less than principle)
                        {
                            assetAtRetire = principle;
                            temp = principle;
                        }

                        minWithdrawal = CalcWithdrawal(rate, assetAtRetire, deathAge - j + 1, taxType, status, principle);
                    }
                    if (j < retireAge)
                    {
                        temp = (temp+amountWithFees) * Math.Pow(1 + rate, 1);
                        principle += amountWithFees;
                        account[j] = temp;
                    }
                    if (j >= retireAge)
                    {
                      //  withdrawalSum += CalcWithdrawal(rate, temp, deathAge - j, taxType, status, amount);
                        

                        double withdrawal = CalcWithdrawal(rate, temp, deathAge - j + 1, taxType, status, principle / (deathAge - retireAge));
                        //  withdrawalSum += CalcWithdrawal(rate, temp, deathAge-j, taxType, status, amount);
                        if (isGMWB)
                        {
                            if (withdrawal < minWithdrawal)
                            {
                                withdrawal = minWithdrawal;
                            }
                        }
                        temp -= (withdrawal-withdrawal*.03);
                        temp = temp * Math.Pow(1 + rate, 1);
                        account[j] = temp;
                    }
                }
                // trials[i] = withdrawalSum / (deathAge - retireAge);
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
