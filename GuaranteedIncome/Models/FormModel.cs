using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GuaranteedIncome.Models
{
    public class FormModel
    {
        public int CurrentAge { get; set; } = 0;

        public int RetireAge { get; set; }
        
        public Boolean isMale { get; set; }

        public double Income { get; set; }
        
        [Required]
        [EnumDataType(typeof(FilingStatus))]
        public FilingStatus FilingStatus { get; set; }

        public double Amount { get; set; }

        [Required]
        public List<Riders> Riders { get; set; }

        [EnumDataType(typeof(FilingStatus))]
        public TaxStatus TaxType { get; set; }//how the funds being added can be taxed : tax free, qualified, unqualified

        public int WithdrawalUntil { get; set; }//zero if you get withdrawal until death age . any other value for fixed withdrawal timeline

        public Boolean isDeferred { get; set; }
    }
}
