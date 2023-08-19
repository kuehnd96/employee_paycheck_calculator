using Api.Interfaces;
using Api.Models;

namespace Api.BusinessLogic
{
    public class DependentBenefitDeduction : IDeduction
    {
        private const decimal DependentBenefitDeductionAmountPerMonth = 600m;

        public decimal CalculatePaycheckDeduction(Employee? employee, int paychecksPerYear)
        {
            ArgumentNullException.ThrowIfNull(employee, nameof(employee));

            if (paychecksPerYear < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(paychecksPerYear), "Paychecks per year must be greater than zero.");
            }

            if (employee.Dependents.Count == 0) return 0.00m;
            
            var deductionPerMonth = employee.Dependents.Count * DependentBenefitDeductionAmountPerMonth;

            var deduction = (12 * deductionPerMonth) / Convert.ToDecimal(paychecksPerYear);

            return Math.Round(deduction, 2);
        }
    }
}
