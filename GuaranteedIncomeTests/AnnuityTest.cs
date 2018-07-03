using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using GuaranteedIncome.Models;

namespace GuaranteedIncomeTests
{
    public class AnnuityTest
    {

        [Fact]
        public void DeffFixedTesting()
        {
            //double[] arr = ImmediateVariable.CalculateReturns(30, 65, 90, .05, .02, 30000, TaxStatus.roth, FilingStatus.Joint,100000);
            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine("i: " + i + " value: " + arr[i]);
            //}

            FormModel myModel = new FormModel();
            myModel.CurrentAge = 61;
            myModel.RetireAge = 65;
            myModel.isMale = false;
            myModel.Income = 80000;
            myModel.FilingStatus = FilingStatus.Joint;
            myModel.Amount = 150000;
            myModel.TaxType = TaxStatus.roth;
            myModel.isDeferred = false;
            myModel.WithdrawalUntil = 20;
            Setup s = new Setup(myModel);
            Data data = s.ReturnData();
            double[] arrF = data.Fixed;
           // double[] arrV = data.Variable.First();
            //double[] arrFI = data.FixedIndexed.First();
            //double[] b = data.Brokerage.First();

            Assert.Equal(114638, arrF[65]);
        }
    }
}
