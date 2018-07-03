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
            return averages[500 / 2];//returns the median
        }
    }
}
