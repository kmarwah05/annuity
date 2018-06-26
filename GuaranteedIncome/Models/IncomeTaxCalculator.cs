using GuaranteedIncome.Models;
using System;
using System.Linq;

namespace tax_planning.Models.Tax_Calculation
{
    public class IncomeTaxCalculator
    {
        public static decimal CapitalGainsTaxFor(FilingStatus status, decimal gain, decimal income)
        {
            income -= GetStandardDeduction(filingStatus: status, jurisdiction: "Federal");
            var bracket = TaxBrackets.CapitalGainsBracketFor(status, income);
            var rate = TaxBrackets.CapitalGainsRateForBracket[bracket];
            return gain * rate;
        }

        public static decimal TotalIncomeTaxFor(FilingStatus status, decimal income, decimal basicAdjustment) =>
            FederalTaxFor(status, income, basicAdjustment) + VaStateTaxFor(status, income);

        public static decimal FederalTaxFor(FilingStatus status, decimal income, decimal basicAdjustment) => CalculateGraduatedTaxFor(status, "Federal", income, basicAdjustment);

        public static decimal VaStateTaxFor(FilingStatus status, decimal income) => CalculateGraduatedTaxFor(status, "VA State", income, 0.00M);

        private static decimal CalculateGraduatedTaxFor(FilingStatus status, string jurisdiction, decimal income, decimal basicAdjustment)
        {
            // Get jurisdiction data
            (decimal lowerBound, decimal upperBound)[] brackets;
            decimal[] rateForBracket;

            switch (jurisdiction)
            {
                case "Federal":
                    brackets = TaxBrackets.FederalIncomeBracketsFor(filingStatus: status);
                    rateForBracket = TaxBrackets.FederalIncomeRateForBracket.ToArray();
                    break;
                case "VA State":
                    brackets = TaxBrackets.VaStateBrackets();
                    rateForBracket = TaxBrackets.VaStateIncomeRateForBracket.ToArray();
                    break;
                default:
                    Console.WriteLine("Jurisdiction not supported");
                    return 0.00M;
            }

            income = GetAdjustedGrossIncome(status, income, jurisdiction);

            // After standard deduction
            var ranges = brackets.Select(bracket => bracket.upperBound - bracket.lowerBound);

            var tax = 0.00M;

            var i = 0;
            while (income > brackets[i].upperBound)
            {
                tax += ranges.ElementAt(i) * rateForBracket[i];
                i++;
            }

            var cherryOnTop = (income - brackets[i].lowerBound - basicAdjustment) * rateForBracket[i];

            var total = tax + cherryOnTop;
            total = total > 0 ? total : 0;

            return total;
        }

        public static decimal GetAdjustedGrossIncome(FilingStatus status, decimal income, string jurisdiction)
        {
            income -= GetStandardDeduction(filingStatus: status, jurisdiction: jurisdiction);
            return income < 0.00M ? 0.00M : income;
        }

        private static decimal GetStandardDeduction(FilingStatus filingStatus, string jurisdiction)
        {
            var standardDeduction = 0.00M;

            switch (jurisdiction)
            {
                case "Federal":
                    switch (filingStatus)
                    {
                        case FilingStatus.Joint:
                            standardDeduction = 24000.00M;
                            break;
                        case FilingStatus.HeadOfHousehold:
                            standardDeduction = 18000.00M;
                            break;
                        case FilingStatus.MarriedSeparate:
                        case FilingStatus.Unmarried:
                        default:
                            standardDeduction = 12000;
                            break;
                    }
                    break;
                case "VA State":
                    switch (filingStatus)
                    {
                        case FilingStatus.Joint:
                            standardDeduction = 6000.00M;
                            break;
                        case FilingStatus.HeadOfHousehold:
                            standardDeduction = 4500.00M;
                            break;
                        case FilingStatus.Unmarried:
                        case FilingStatus.MarriedSeparate:
                        default:
                            standardDeduction = 3000.00M;
                            break;
                    }
                    break;
                case "Capital Gains":
                    break;
                default:
                    Console.WriteLine("Jurisdiction not supported");
                    return 0.00M;
            }
            return standardDeduction;
        }
    }
}
