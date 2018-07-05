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
            (double age, double lifeExpectancy)[] life = LifeExpectancy.GenderLifeExpectancy(Gender);

            if (Gender == Gender.Male)
            {
                deathAge = Convert.ToInt32(age + life[age - 6].lifeExpectancy);
            }
            else
            {
                deathAge = Convert.ToInt32(age + life[age - 11].lifeExpectancy);
            }


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

           

            income = myModel.Income;
            status = myModel.FilingStatus;
            isDeferred = myModel.isDeferred;
            amount = myModel.Amount;
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
                data.Variable = dv.CalculateReturns(age, retireAge, WithdrawalUntil, MarketData.VariableRate, MarketData.VariableDeviation, amount, TaxType, status, income,Riders);
                data.VariableMedian = AverageMedian.calcAverageMedian(data.Variable[data.Variable.Count - 1]);//accesses the last array in the list that stores the averages of all the trials, and inputs this into method that computes median

                //Fixed Indexed Calculations
                data.FixedIndexed = dfi.CalculateReturns(age, retireAge, WithdrawalUntil, MarketData.FixedIndexedRate, MarketData.FixedIndexedDeviation, amount, TaxType, status, income,Riders);
                data.FixedIndexedMedian = AverageMedian.calcAverageMedian(data.FixedIndexed[data.FixedIndexed.Count - 1]);//accesses the last array in the list that stores the averages of all the trials, and inputs this into method that computes median

                //brokerage calculations
                data.Brokerage = b.CalculateReturns(age, retireAge, WithdrawalUntil, MarketData.BrokerageRate, MarketData.BrokerageDeviation, amount,0, TaxStatus.qualified, status, income,Riders);
                data.BrokerageMedian = AverageMedian.calcAverageMedian(data.Brokerage[data.Brokerage.Count - 1]);//accesses the last array in the list that stores the averages of all the trials, and inputs this into method that computes median

                data.FixedIndexAboveBrokerage =1- AverageMedian.calcPercentageAbove(data.Brokerage[data.Brokerage.Count - 1], data.FixedIndexedMedian);
                data.VariableAboveBrokerage =1- AverageMedian.calcPercentageAbove(data.Brokerage[data.Brokerage.Count - 1], data.VariableMedian);
                data.BrokerageBelowFixed =1- AverageMedian.calcPercentageAbove(data.Brokerage[data.Brokerage.Count - 1], data.Fixed);



                data.Brokerage.RemoveAt(data.Brokerage.Count - 1);//removes the last elemtn of the list which was just used to calculate the median
                data.Variable.RemoveAt(data.Variable.Count - 1);//removes the last elemtn of the list which was just used to calculate the median
                data.FixedIndexed.RemoveAt(data.FixedIndexed.Count - 1);//removes the last elemtn of the list which was just used to calculate the median


            }
            else
            {
                ImmediateFixed df = new ImmediateFixed();
                ImmediateVariable dv = new ImmediateVariable();
                ImmediateFixedIndexed dfi = new ImmediateFixedIndexed();
                Brokerage b = new Brokerage();
                data.Fixed = df.CalculateReturns(age, retireAge, WithdrawalUntil, MarketData.FixedRate, MarketData.FixedDeviation, amount, TaxType, status, income,Riders);
              
                //Variable calculations
                data.Variable = dv.CalculateReturns(age, retireAge, WithdrawalUntil, MarketData.VariableRate, MarketData.VariableDeviation, amount, TaxType, status, income, Riders);
                data.VariableMedian = AverageMedian.calcAverageMedian(data.Variable[data.Variable.Count - 1]);//accesses the last array in the list that stores the averages of all the trials, and inputs this into method that computes median

                //Fixed Indexed Calculations
                data.FixedIndexed = dfi.CalculateReturns(age, retireAge, WithdrawalUntil, MarketData.FixedIndexedRate, MarketData.FixedIndexedDeviation, amount, TaxType, status, income, Riders);
                data.FixedIndexedMedian = AverageMedian.calcAverageMedian(data.FixedIndexed[data.FixedIndexed.Count - 1]);//accesses the last array in the list that stores the averages of all the trials, and inputs this into method that computes median

                //brokerage calculations
                data.Brokerage = b.CalculateReturns(age, retireAge, WithdrawalUntil, MarketData.BrokerageRate, MarketData.BrokerageDeviation, 0, amount, TaxStatus.qualified, status, income, Riders);
                data.BrokerageMedian = AverageMedian.calcAverageMedian(data.Brokerage[data.Brokerage.Count - 1]);//accesses the last array in the list that stores the averages of all the trials, and inputs this into method that computes median

              
                data.VariableAboveBrokerage = AverageMedian.calcPercentageAbove(data.Brokerage[data.Brokerage.Count - 1], data.VariableMedian);
                data.FixedIndexAboveBrokerage = AverageMedian.calcPercentageAbove(data.Brokerage[data.Brokerage.Count - 1], data.FixedIndexedMedian);
                data.BrokerageBelowFixed =1- AverageMedian.calcPercentageAbove(data.Brokerage[data.Brokerage.Count - 1], data.Fixed);

                data.Brokerage.RemoveAt(data.Brokerage.Count - 1);//removes the last elemtn of the list which was just used to calculate the median
                data.FixedIndexed.RemoveAt(data.FixedIndexed.Count - 1);//removes the last elemtn of the list which was just used to calculate the median
                data.Variable.RemoveAt(data.Variable.Count - 1);//removes the last elemtn of the list which was just used to calculate the median



            }

            return data;
        }
    }
}
