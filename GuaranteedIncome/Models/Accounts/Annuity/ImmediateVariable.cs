using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class ImmediateVariable:Account
    {
        public override List<double[]> CalculateReturns(int age,int retireAge, int deathAge, double mean, double stdDeviation,double amount, TaxStatus taxType, FilingStatus status,double income,List<Riders> Riders)
        {
            //Fees added to death benefit rider
            double deathBenefitPenaltyFee = amount;
            Boolean isDeathBenefit;
            if (Riders.Contains(Models.Riders.DeathBenefit))
            {
                isDeathBenefit = true;
                deathBenefitPenaltyFee -= deathBenefitPenaltyFee * .005;
            }
            else
            {
                isDeathBenefit = false;
            }

            double amountWithFees = amount;
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
            if (Riders.Contains(Models.Riders.GMAB))//checks to see if GMAB is a rider
            {
                isGMAB = true;
                amountWithFees -= amountWithFees * .005;
            }
            else
            {
                isGMAB = false;
            }
            List<double[]> trials = new List<double[]>();
            double[] account = new double[150];
            for (int i = 0; i < 100; i++)
            {
                double temp = amountWithFees;
                double principle = amountWithFees;
                double withdrawalSum=0;
                double minWithdrawal = 0;
                for (int j = age; j < deathAge; j++)
                { Random rand = new Random();
                    double rate = mean + stdDeviation * (rand.NextDouble() * (6) - 3);
                    if (j == retireAge)
                    {
                        double assetAtRetire = temp;
                        if (assetAtRetire < principle && isGMAB && isDeathBenefit)//if they have GMAB then set Amount in Annuity equal to principle. (only if amount is less than principle)
                        {
                            assetAtRetire = principle;
                            temp = principle;
                        }

                         minWithdrawal = CalcWithdrawal(rate, assetAtRetire, deathAge - j + 1, taxType, status, principle);
                    }
                
                    if (j < retireAge)
                    {
                        temp = temp * Math.Pow(1 + rate, 1);
                        account[j]=temp;
                    }
                    if(j>=retireAge)
                    {

                        double withdrawal = CalcWithdrawal(rate, temp, deathAge - j + 1, taxType, status, principle / (deathAge - retireAge));
                        //  withdrawalSum += CalcWithdrawal(rate, temp, deathAge-j, taxType, status, amount);
                        if (isGMWB)
                        {
                            if (withdrawal< minWithdrawal)
                            {
                                withdrawal = minWithdrawal;
                            }
                        }
                        temp -= (withdrawal-withdrawal*.03);
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
        public override double CalcWithdrawal(double rate, double presentValue,int yearsWithdrawing, TaxStatus taxType,FilingStatus status,double principle)
        {
            return  TaxHelper.CalcTaxedWithdrawals(rate, presentValue, yearsWithdrawing, taxType, status, principle);
        }
    }
}
