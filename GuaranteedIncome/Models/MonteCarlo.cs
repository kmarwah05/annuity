using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public static class MonteCarlo
    {

        public static double[] populateArray(double mean, double stdDeviation, Boolean isDeferred, double amount, int time)
        {
            
            double[] arr = new double[1000];
            double randomNumber = 0;


            //if deferred
            if (isDeferred == true)
            {
                for (int i = 0; i < 1000; i++)
                {
                    double temp = amount * (mean + stdDeviation * randomNumber);
                    for (int j = 1; j <= time; j++)
                    {
                        randomNumber = BoxMuller();
                        temp+= (temp + amount) * (mean + stdDeviation * randomNumber);
                    }
                    arr[i] = temp;
                }
            }
            //if immediate
            else
            {
                for (int i = 0; i < 1000; i++)
                {
                    arr[i] = amount * (mean * time + stdDeviation * BoxMuller() * Math.Sqrt(time));
                }
            }

            return arr;

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
