using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class Data
    {
        public double VariableMedian;
        public double FixedIndexedMedian;
        public double BrokerageMedian;


        public double FixedIndexAboveBrokerage;//fixed index is above average brokerage
        public double VariableAboveBrokerage;
        public double BrokerageBelowFixed;//amount of time brokerage is less than fixed


        public double Fixed;
        public List<double[]> Variable;
        public List<double[]> FixedIndexed;
        public List<double[]> Brokerage;


        
    }
}
