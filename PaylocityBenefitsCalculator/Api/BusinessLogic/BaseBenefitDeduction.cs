using Api.Interfaces;
using Api.Models;

namespace Api.BusinessLogic
{
    public class BaseBenefitDeduction : IDeduction
    {
        private const decimal BenefitDeductionAmountPerMonth = 1000m; 
        
        public decimal CalculatePaycheckDeduction(Employee? employee, int paychecksPerYear)
        {
            ArgumentNullException.ThrowIfNull(employee, nameof(employee));
            
            if (paychecksPerYear < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(paychecksPerYear), "Paychecks per year must be greater than zero.");
            }
            
            var deduction = (12 * BenefitDeductionAmountPerMonth) / Convert.ToDecimal(paychecksPerYear);

            return Math.Round(deduction, 2);
        }
    }
}
