using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public abstract class Account
    {
         public abstract decimal[] CalculateReturns();
    }
}
