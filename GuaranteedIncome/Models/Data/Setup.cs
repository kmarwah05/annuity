using GuaranteedIncome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class Setup
    {
        public int age;
        public int retireAge;
        public int deathAge;
        public Gender Gender;
        public double income;
        public FilingStatus status;
        public double amount;
        public TaxStatus TaxType;
        public List<Riders> Riders;
        public Boolean isDeferred;

        public int WithdrawalUntil;


        public Setup(FormModel myModel)
        {

            age = myModel.CurrentAge;
            retireAge = myModel.RetireAge;
            if (myModel.isMale)
            {
                Gender = Gender.Male;
            }
            else
            {
                Gender = Gender.Female;
            }
            (double age, double lifeExpectancy)[] life = LifeExpectancy.GenderLifeExpectancy(Gender);
            
           
                deathAge = Convert.ToInt32(age + life[age].lifeExpectancy);
            

            Riders = myModel.Riders;
            if (myModel.WithdrawalUntil==0)
            {
                WithdrawalUntil = deathAge;
            }
            else
            {
                WithdrawalUntil = retireAge + myModel.WithdrawalUntil;
            }
            if (myModel.isMale)
            {
                Gender = Gender.Male;
            }
            else
            {
                Gender = Gender.Female;
            }

            TaxType = myModel.TaxType;
            Console.WriteLine(myModel.TaxType);
            income = myModel.Income;
            status = myModel.FilingStatus;
            isDeferred = myModel.isDeferred;
            amount = myModel.Amount;
            if (isDeferred)
            {
                TaxType = TaxStatus.unqualified;
            }
        }

        public Data ReturnData()
        {
            Data data = new Data();
            if (isDeferred == true)
            {
                DeferredFixed df = new DeferredFixed();
                DeferredVariable dv = new DeferredVariable();
                DeferredFixedIndexed dfi = new DeferredFixedIndexed();
                Brokerage b = new Brokerage();
                
                data.Fixed = df.CalculateReturns(age, retireAge, WithdrawalUntil, MarketData.FixedRate,MarketData.FixedDeviation, amount, TaxType, status, income,Riders);


                //Variable calculations
                double[] VariableSorted=Sort.mergeSort(dv.CalculateReturns(age, retireAge, WithdrawalUntil, MarketData.FixedIndexedRate, MarketData.FixedIndexedDeviation, amount, TaxType, status, income, Riders));
                data.VariableMedian = VariableSorted[4000 / 2];
                data.VariableLowerQuartile = VariableSorted[4000 / 4];
                data.VariableUpperQuartile = VariableSorted[4000*3/4];


                //Fixed Indexed Calculations
                double[] FixedIndexedSorted = Sort.mergeSort(dfi.CalculateReturns(age, retireAge, WithdrawalUntil, MarketData.FixedIndexedRate, MarketData.FixedIndexedDeviation, amount, TaxType, status, income, Riders));
                data.FixedIndexedMedian = FixedIndexedSorted[4000 / 2];
                data.FixedIndexedLowerQuartile = FixedIndexedSorted[4000 / 4];
                data.FixedIndexedUpperQuartile = FixedIndexedSorted[4000*3/ 4];



                //brokerage calculations
                double[] BrokerageSorted = Sort.mergeSort(b.CalculateReturns(age, retireAge, WithdrawalUntil, MarketData.BrokerageRate, MarketData.BrokerageDeviation, amount,0, TaxStatus.qualified, status, income, Riders));
                data.BrokerageMedian = BrokerageSorted[4000 / 2];
                data.BrokerageLowerQuartile = BrokerageSorted[4000 / 4];
                data.BrokerageUpperQuartile = BrokerageSorted[4000*3 / 4];



                data.FixedIndexAboveBrokerage =1-AverageMedian.calcPercentageAbove(BrokerageSorted, data.FixedIndexedMedian);
                data.VariableAboveBrokerage =1-AverageMedian.calcPercentageAbove(BrokerageSorted, data.VariableMedian);
                data.BrokerageBelowFixed =1- AverageMedian.calcPercentageAbove(BrokerageSorted, data.Fixed);


            }
            else
            {


                ImmediateFixed df = new ImmediateFixed();
                ImmediateVariable dv = new ImmediateVariable();
                ImmediateFixedIndexed dfi = new ImmediateFixedIndexed();
                Brokerage b = new Brokerage();

                data.Fixed = df.CalculateReturns(age, retireAge, WithdrawalUntil, MarketData.FixedRate, MarketData.FixedDeviation, amount, TaxType, status, income, Riders);

                Console.WriteLine(TaxType);
                //Variable calculations
                double[] VariableSorted = Sort.mergeSort(dv.CalculateReturns(age, retireAge, WithdrawalUntil, MarketData.FixedIndexedRate, MarketData.FixedIndexedDeviation, amount, TaxType, status, income, Riders));
                data.VariableMedian = VariableSorted[4000 / 2];
                data.VariableLowerQuartile = VariableSorted[4000 / 4];
                data.VariableUpperQuartile = VariableSorted[4000*3 / 4];


                //Fixed Indexed Calculations
                double[] FixedIndexedSorted = Sort.mergeSort(dfi.CalculateReturns(age, retireAge, WithdrawalUntil, MarketData.FixedIndexedRate, MarketData.FixedIndexedDeviation, amount, TaxType, status, income, Riders));
                data.FixedIndexedMedian = FixedIndexedSorted[4000 / 2];
                data.FixedIndexedLowerQuartile = FixedIndexedSorted[4000 / 4];
                data.FixedIndexedUpperQuartile = FixedIndexedSorted[4000*3/ 4];



                //brokerage calculations
                double[] BrokerageSorted = Sort.mergeSort(b.CalculateReturns(age, retireAge, WithdrawalUntil, MarketData.BrokerageRate, MarketData.BrokerageDeviation, 0, amount, TaxStatus.qualified, status, income, Riders));
                data.BrokerageMedian = BrokerageSorted[4000 / 2];
                data.BrokerageLowerQuartile = BrokerageSorted[4000 / 4];
                data.BrokerageUpperQuartile = BrokerageSorted[4000*3/ 4];



                data.FixedIndexAboveBrokerage = 1 - AverageMedian.calcPercentageAbove(BrokerageSorted, data.FixedIndexedMedian);
                data.VariableAboveBrokerage = 1 - AverageMedian.calcPercentageAbove(BrokerageSorted, data.VariableMedian);
                data.BrokerageBelowFixed = 1 - AverageMedian.calcPercentageAbove(BrokerageSorted, data.Fixed);
            }

            return data;
        }
    }
}
