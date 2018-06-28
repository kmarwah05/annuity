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

        public int DeathAge { get; set; }

        public bool Gender { get; set; }

        public double Income { get; set; }

        [Required]
        [EnumDataType(typeof(FilingStatus))]
        public FilingStatus FilingStatus { get; set; }

        public double InitialAmount { get; set; }

        public double YearlyAdditioins { get; set; }

        [Required]
        [EnumDataType(typeof(Riders))]
        public IEnumerable<Riders> Riders { get; set; }

        public TaxStatus TaxType { get; set; }//how the funds being added can be taxed : tax free, qualified, unqualified

        public int WithdrawalUntil { get; set; }//zero if you get withdrawal until death age . any other value for fixed withdrawal timeline
    }
}
