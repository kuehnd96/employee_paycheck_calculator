using Api.Models;

namespace Api.Interfaces
{
    /// <summary>
    /// Calculates the amount all paychecks will be for the year for an employee.
    /// </summary>
    public interface IPaycheckCalculator
    { 
        /// <summary>
        /// Calculate paycheck amount for employee.
        /// </summary>
        /// <param name="employee"><see cref="Employee"/> to calculate paycheck amount for. Cannot be null.</param>
        /// <returns>The amount of each paycheck for the year.</returns>
        decimal CalculatePaycheckAmount(Employee employee);

        // why: I had this method return a Task<decimal> at first to allow for 
        //  async implementations but decided to keep it synchronous.
        // This should be changed to be async if 1) This implementation holds the 
        //  running thread too long from the WebApi controller or 2) if there is a need 
        // for an implementation of this interface that should run on another thread
        // (call to web service, call to complex/long-running calculation, etc.)
    }
}
