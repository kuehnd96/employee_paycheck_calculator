using Api.BusinessLogic;
using Api.Models;
using System;
using Xunit;

namespace ApiTests.UnitTests.Deductions
{
    public class DependentBenefitDeductionTests
    {
        private readonly DependentBenefitDeduction _subject;

        public DependentBenefitDeductionTests()
        {
            _subject = new DependentBenefitDeduction();
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
            decimal expectedDeductionAmount = 830.77m;

            // ARRANGE
            var employee = new Employee()
            {
                Id = 1,
                Dependents = new[]
                {
                    new Dependent(),
                    new Dependent(),
                    new Dependent()
                }
            };
            
            // ACT
            var deductionAmount = _subject.CalculatePaycheckDeduction(employee, 26);

            // MEASURE
            Assert.Equal(expectedDeductionAmount, deductionAmount);
        }

        [Fact]
        public void CalculatePaycheckDeduction_ZeroDependents_ReturnsExpectedDeduction()
        {
            decimal expectedDeductionAmount = 0.00m;

            // ACT
            var deductionAmount = _subject.CalculatePaycheckDeduction(new Employee(), 26);

            // MEASURE
            Assert.Equal(expectedDeductionAmount, deductionAmount);
        }
    }
}
