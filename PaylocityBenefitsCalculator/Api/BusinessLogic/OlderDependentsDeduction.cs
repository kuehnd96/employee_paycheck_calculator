using Api.Interfaces;
using Api.Models;

namespace Api.BusinessLogic
{
    public class OlderDependentsDeduction : IDeduction
    {
        private const int DependentAge = -50;
        private const int DeductionMonthlyAmount = 200;
        
        public decimal CalculatePaycheckDeduction(Employee? employee, int paychecksPerYear)
        {
            ArgumentNullException.ThrowIfNull(employee, nameof(employee));

            if (paychecksPerYear < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(paychecksPerYear), "Paychecks per year must be greater than zero.");
            }

            var birthdateThreshold = DateTime.Now.AddYears(DependentAge);
            var olderDependents = employee
                .Dependents
                .Where(d => d.DateOfBirth < birthdateThreshold);

            if (!olderDependents.Any()) return 0.00m;

            var deductionPerMonth = olderDependents.Count() * DeductionMonthlyAmount;

            var deduction = (12 * deductionPerMonth) / Convert.ToDecimal(paychecksPerYear);

            return Math.Round(deduction, 2);
        }
    }
}
