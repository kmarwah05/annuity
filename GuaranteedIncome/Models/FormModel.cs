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

        public int DeathAge { get; set; }

        public bool Gender { get; set; }

        public decimal? Income { get; set; }

        [Required]
        [EnumDataType(typeof(FilingStatus))]
        public FilingStatus? FilingStatus { get; set; }

        public decimal? InitialAmount { get; set; }

        public decimal? YearlyAdditioins { get; set; }

        [Required]
        //public Riders? Riders { get; set; }

        public decimal? Qualified { get; set; }

        public decimal? NonQualified { get; set; }

        public decimal? Roth { get; set; }


    }
}
