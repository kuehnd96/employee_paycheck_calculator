using Api.BusinessLogic;
using Api.Models;
using System;
using Xunit;

namespace ApiTests.UnitTests.Deductions
{
    public class HighSalaryDeductionTests
    {
        private readonly HighSalaryDeduction _subject;

        public HighSalaryDeductionTests()
        {
            _subject = new HighSalaryDeduction();
        }

        [Fact]
        public void CalculatePaycheckDeduction_NullEmployee()
        {
            // ACT
            Assert.Throws<ArgumentNullException>(() => _subject.CalculatePaycheckDeduction(null, 23));
        }

        [Fact]
        public void CalculatePaycheckDeduction_InvalidPaychecksPerYear()
        {
            // ACT
            Assert.Throws<ArgumentOutOfRangeException>(() => _subject.CalculatePaycheckDeduction(new Employee(), -1));
        }

        [Fact]
        public void CalculatePaycheckDeduction_ReturnsExpectedDeduction()
        {
            decimal expectedDeductionAmount = 69.23m;

            // ARRANGE
            var employee = new Employee() { Salary = 90000m };
            
            // ACT
            var deductionAmount = _subject.CalculatePaycheckDeduction(employee, 26);

            // MEASURE
            Assert.Equal(expectedDeductionAmount, deductionAmount);
        }

        [Fact]
        public void CalculatePaycheckDeduction_SalaryTooLow_ReturnsZeroDeduction()
        {
            decimal expectedDeductionAmount = 0.00m;

            // ARRANGE
            var employee = new Employee() { Salary = 79500m };

            // ACT
            var deductionAmount = _subject.CalculatePaycheckDeduction(employee, 26);

            // MEASURE
            Assert.Equal(expectedDeductionAmount, deductionAmount);
        }
    }
}
