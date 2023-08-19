using Api.Interfaces;
using Api.Models;

namespace Api.BusinessLogic
{
    public class HighSalaryDeduction : IDeduction
    {
        private const decimal SalaryMinimum = 80000m;
        private const decimal DeductionPercentage = 0.02m;

        public decimal CalculatePaycheckDeduction(Employee? employee, int paychecksPerYear)
        {
            ArgumentNullException.ThrowIfNull(employee, nameof(employee));

            if (paychecksPerYear < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(paychecksPerYear), "Paychecks per year must be greater than zero.");
            }

            if (employee.Salary < SalaryMinimum) return 0m;

            var deduction = (employee.Salary * DeductionPercentage) / paychecksPerYear;

            return Math.Round(deduction, 2);
        }
    }
}
