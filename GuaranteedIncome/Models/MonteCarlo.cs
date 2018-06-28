using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public static class MonteCarlo
    {
        public static double generateInterestRate(double mean, double stdDeviation)
        {
            return (BoxMuller() * stdDeviation + mean);
        }
    

        public static double BoxMuller()
        {
            double result_value = 0;
            double x1, x2, squared;
           
            do
            {
                System.Random rand = new System.Random();
                int a = rand.Next(1,2);
                x1 = 2.0 * rand.NextDouble() - 1.0;
                x2 = 2.0 * rand.NextDouble() - 1.0;
                squared = x1 * x1 + x2 * x2;
            }
            while (squared >= 1.0);

            result_value = x1 * Math.Sqrt(-2 * Math.Log(squared) / squared);

            return result_value;
        }

    }
}
