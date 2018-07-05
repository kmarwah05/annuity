using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class DeferredVariable
    {
        public List<double[]> CalculateReturns(int age, int retireAge, int deathAge, double mean, double stdDeviation, double amount, TaxStatus taxType, FilingStatus status, double income,List<Riders> Riders)
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
            double principle = 0;
            Boolean isGMWB;
            if (Riders.Contains(Models.Riders.GMWB))//Checks to see if GMWB is a rider and charges fee
            {
                isGMWB = true;
                amountWithFees -= amountWithFees * .005;
            }
            else
            {
                isGMWB = false;
            }

            Boolean isGMAB;
            if (Riders.Contains(Models.Riders.GMAB))//checks to see if GMAb is a rider and charges fee
            {
                isGMAB = true;
                amountWithFees -= amountWithFees * .005;
            }
            else
            {
                isGMAB = false;
            }

            Boolean isDeath;
            if (Riders.Contains(Models.Riders.DeathBenefit))//checks to see if death benefit is a rider and charges fee
            {
                isDeath = true;
                amountWithFees -= amountWithFees * .005;
            }
            else
            {
                isDeath = false;
            }

            List<double[]> trials = new List<double[]>();
            double[] MedianAverageWithdrawal = new double[2000];
            for (int i = 0; i < 2000; i++)
            {
                double[] account = new double[deathAge-retireAge];
                int count = 0;
                double temp = 0;
                double minWithdrawal = 0;//minimum withdrawal. used if they have the GMWB rider
                double withdrawalAmount = 0;

                for (int j = age; j < deathAge; j++)
                {
                    Random rand = new Random();
                    double rate = mean + stdDeviation * (rand.NextDouble() * (6) - 3);//random number from -3 to 3 
                    if (j == retireAge)
                    {
                        double assetAtRetire = temp;
                        if (assetAtRetire < principle && isGMAB)//if they have GMAB then set Amount in Annuity equal to principle. (only if amount is less than principle)
                        {
                            assetAtRetire = principle;
                            temp = principle;
                        }
                        minWithdrawal = CalcWithdrawal(rate, assetAtRetire, deathAge - j + 1, taxType, status, principle);//minimum withdrawal used for GMWB rider
                    }
                    if (j < retireAge)
                    {
                        temp = (temp+amountWithFees) * Math.Pow(1 + rate, 1);
                        principle += amountWithFees;
                    }
                    if (j >= retireAge)
                    {

                        
                        double withdrawal = CalcWithdrawal(mean, temp, deathAge - j + 1, taxType, status, principle / (deathAge - retireAge));
                        withdrawal = withdrawal - withdrawal * withdrawalPercentageFee;
                        temp -= withdrawal;
                        withdrawal = withdrawal - withdrawal * .04;//3% fee for variable account
                        if (isGMWB)//if they have the GMWb rider then they won't have a withdrawal less than principle
                        {
                            if (withdrawal < minWithdrawal)
                            {
                                withdrawal = minWithdrawal;
                            }
                        }

                        account[count] = withdrawal;
                        withdrawalAmount+= withdrawal;
                       
                        temp = temp * Math.Pow(1 + rate, 1);
                        count++;
                        
                    }
                }
                if (i < 50)
                {
                    trials.Add(account);
                }
                withdrawalAmount = withdrawalAmount / (deathAge - retireAge);//calculates average withdrawal
                MedianAverageWithdrawal[i] = withdrawalAmount;//stores the average withdrawal for this trial
            }
            trials.Add(MedianAverageWithdrawal);//adds an array of the averages to the end of the lsit, will be taken out later and used
            return trials;
        }


        public double CalcWithdrawal(double rate, double presentValue, int yearsWithdrawing, TaxStatus taxType, FilingStatus status, double principle)
        {
            return TaxHelper.CalcTaxedWithdrawals(rate, presentValue, yearsWithdrawing, taxType, status, principle);
        }

    }

}
