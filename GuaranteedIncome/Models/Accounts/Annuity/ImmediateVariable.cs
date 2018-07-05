using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class ImmediateVariable
    {
        public double[] CalculateReturns(int age,int retireAge, int deathAge, double mean, double stdDeviation,double amount, TaxStatus taxType, FilingStatus status,double income,List<Riders> Riders)
        {//same as deferred variable except lumpsum instead of continuous payments
           

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
          //  List<double[]> trials = new List<double[]>();
            double[] MedianAverageWithdrawal = new double[4000];

            for (int i = 0; i < 4000; i++)
            {
                double[] account = new double[deathAge-retireAge];
                int count = 0;
                double temp = amountWithFees;
                double principle = amountWithFees;
                double minWithdrawal = 0;
                double withdrawalAmount = 0;
                for (int j = age; j < deathAge; j++)
                { Random rand = new Random();
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
                        temp = temp * Math.Pow(1 + rate, 1);
                    }
                    else
                    {

                        double withdrawal = CalcWithdrawal(mean, temp, deathAge - j + 1, taxType, status, principle / (deathAge - retireAge));

                        temp -= withdrawal;

                        withdrawal = withdrawal - withdrawal * .03;
                        if (isGMWB)
                        {
                            if (withdrawal< minWithdrawal)
                            {
                                withdrawal = minWithdrawal;
                            }
                        }
                        account[count] = withdrawal;
                        withdrawalAmount += withdrawal;
                        temp = temp * Math.Pow(1 + rate, 1);
                        count++;
                    }
                }
                //if (i < 500)
                //{
                //    trials.Add(account);
                //}
                withdrawalAmount = withdrawalAmount / (deathAge - retireAge);//calculates average withdrawal
                MedianAverageWithdrawal[i] = withdrawalAmount;//stores the average withdrawal for this trial
            }
           // trials.Add(MedianAverageWithdrawal);//adds an array of the averages to the end of the lsit, will be taken out later and used
            return MedianAverageWithdrawal;
        }
        public double CalcWithdrawal(double rate, double presentValue,int yearsWithdrawing, TaxStatus taxType,FilingStatus status,double principle)
        {
            return  TaxHelper.CalcTaxedWithdrawals(rate, presentValue, yearsWithdrawing, taxType, status, principle);
        }
    }
}
