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
        public Boolean isDeferred; 

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


            (double age, double lifeExpectancy)[] life= LifeExpectancy.GenderLifeExpectancy(Gender);

            if(Gender==Gender.Male)
            deathAge = Convert.ToInt32(age + life[age + 7].lifeExpectancy);
            else
            deathAge = Convert.ToInt32(age + life[age + 12].lifeExpectancy);

            income = myModel.Income;
            status = myModel.FilingStatus;
            isDeferred = myModel.isDeferred;
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
                data.Fixed = df.CalculateReturns(age, retireAge, deathAge, .05, 0, amount, TaxType, status, income);
                data.Variable = dv.CalculateReturns(age, retireAge, deathAge, .05, 0, amount, TaxType, status, income);
                data.FixedIndexed = dfi.CalculateReturns(age, retireAge, deathAge, .05, 0, amount, TaxType, status, income);
                data.Brokerage = b.CalculateReturns(age, retireAge, deathAge, .05, 0, amount, TaxType, status, income);
            }
            else
            {
                ImmediateFixed df = new ImmediateFixed();
                ImmediateVariable dv = new ImmediateVariable();
                ImmediateFixedIndexed dfi = new ImmediateFixedIndexed();
                Brokerage b = new Brokerage();
                data.Fixed = df.CalculateReturns(age, retireAge, deathAge, .05, 0, amount, TaxType, status, income);
                data.Variable = dv.CalculateReturns(age, retireAge, deathAge, .05, 0, amount, TaxType, status, income);
                data.FixedIndexed = dfi.CalculateReturns(age, retireAge, deathAge, .05, 0, amount, TaxType, status, income);
                data.Brokerage = b.CalculateReturns(age, retireAge, deathAge, .05, 0, amount, TaxType, status, income);
            }
            return data;


        }
    }
}
