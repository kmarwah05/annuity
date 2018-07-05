using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public static class AverageMedian
    {
        public static double calcAverageMedian(double[] averages)
        {
            averages = Sort.mergeSort(averages);//returns sorted array of averages in order to find the median
            return averages[5000 / 2];//returns the median
        }

        public static double calcPercentageAbove(double[]averages,double median)
        {
            double count = 0;
            for(int i = 0; i < averages.Length; i++)
            {

                if (median < averages[i])
                {
                    count++;
                }
            }
            return count / averages.Length;
        }
       
    }
}
