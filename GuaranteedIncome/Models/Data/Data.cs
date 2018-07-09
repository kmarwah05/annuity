using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class Data
    {
        public double Fixed;

        public double VariableLowerQuartile;
        public double VariableMedian;
        public double VariableUpperQuartile;
        public double VariableWorstCase;


        public double FixedIndexedLowerQuartile;
        public double FixedIndexedMedian;
        public double FixedIndexedUpperQuartile;
        public double FixedIndexedWorstCase;

        public double BrokerageLowerQuartile;
        public double BrokerageMedian;
        public double BrokerageUpperQuartile;
        public double BrokerageWorstCase;


        public double FixedIndexAboveBrokerage;//fixed index is above average brokerage
        public double VariableAboveBrokerage;
        public double BrokerageBelowFixed;//amount of time brokerage is less than fixed


               
    }
}
