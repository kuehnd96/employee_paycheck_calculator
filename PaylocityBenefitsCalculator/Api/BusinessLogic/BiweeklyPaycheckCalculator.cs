using Api.Interfaces;
using Api.Models;

namespace Api.BusinessLogic
{
    public class BiweeklyPaycheckCalculator : IPaycheckCalculator
    {
        private const int PaychecksPerYear = 26;
        private readonly IEnumerable<IDeduction> _deductions;

        public BiweeklyPaycheckCalculator(IEnumerable<IDeduction> deductions)
        {
            _deductions = deductions;
        }

        public decimal CalculatePaycheckAmount(Employee employee)
        {
            ArgumentNullException.ThrowIfNull(employee, nameof(employee));
            
            var paycheckDeductionTotal = 0m;

            foreach (var deduction in _deductions)
            {
                paycheckDeductionTotal += deduction.CalculatePaycheckDeduction(employee, PaychecksPerYear);
            }

            return (employee.Salary / PaychecksPerYear) - paycheckDeductionTotal;
        }
    }
}
