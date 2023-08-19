using Api.Models;

namespace Api.Interfaces
{
    /// <summary>
    /// Calculates a paycheck deduction.
    /// </summary>
    public interface IDeduction
    {
        /// <summary>
        /// Calculate a deduction from an employee's paycheck.
        /// </summary>
        /// <param name="employee">The employee to calculate the deduction for. Cannot be null.</param>
        /// <param name="paychecksPerYear">Number of paychecks per year. Must be greater than zero.</param>
        /// <returns>The amount to deduct from the employee's paycheck per pay period.</returns>
        decimal CalculatePaycheckDeduction(Employee? employee, int paychecksPerYear);
    }
}
