using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using GuaranteedIncome.Models;

namespace GuaranteedIncomeTests
{
    public class AnnuityTest
    {
        Data data = new Data();
        public int age;
        public int retireAge;
        public int deathAge;
        public Gender Gender;
        public double income;
        public FilingStatus status;
        public double amount;
        public TaxStatus TaxType;
        public Boolean isDeferred;
        public List<Riders> Riders;

        public Data ReturnData()
        {
            Data data = new Data();
            if (isDeferred == true)
            {
                DeferredFixed df = new DeferredFixed();
                DeferredVariable dv = new DeferredVariable();
                DeferredFixedIndexed dfi = new DeferredFixedIndexed();
                Brokerage b = new Brokerage();
                data.Fixed = df.CalculateReturns(age, retireAge, deathAge, .05, 0, amount, TaxType, status, income, Riders);
                data.Variable = dv.CalculateReturns(age, retireAge, deathAge, .05, 0, amount, TaxType, status, income, Riders);
                data.FixedIndexed = dfi.CalculateReturns(age, retireAge, deathAge, .05, 0, amount, TaxType, status, income, Riders);
                data.Brokerage = b.CalculateReturns(age, retireAge, deathAge, .05, 0, amount, TaxType, status, income, Riders);
            }
            else
            {
                ImmediateFixed df = new ImmediateFixed();
                ImmediateVariable dv = new ImmediateVariable();
                ImmediateFixedIndexed dfi = new ImmediateFixedIndexed();
                Brokerage b = new Brokerage();
                data.Fixed = df.CalculateReturns(age, retireAge, deathAge, .05, 0, amount, TaxType, status, income, Riders);
                data.Variable = dv.CalculateReturns(age, retireAge, deathAge, .05, 0, amount, TaxType, status, income, Riders);
                data.FixedIndexed = dfi.CalculateReturns(age, retireAge, deathAge, .05, 0, amount, TaxType, status, income, Riders);
                data.Brokerage = b.CalculateReturns(age, retireAge, deathAge, .05, 0, amount, TaxType, status, income, Riders);
            }
            return data;
        }
       
        [Fact]
        public void IFAnnuityTest()
        {
            Assert.Equal(168826.32.ToString(),(61, 65, 80, .05, .01, 150000, TaxStatus.qualified, FilingStatus.Unmarried, 80000, Riders).ToString());
        }
    }
}
