using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class DeferredFixed
    {
        public decimal calcYearlyIncome(decimal rate, decimal presentValue, FormModel myModel)
        {
            Setup s = new Setup(myModel);
           return  s.CalcTaxedWithdrawals(rate, presentValue);
        }
    }
}
