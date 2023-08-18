using Api.BusinessLogic;
using Api.Models;
using System;
using Xunit;

namespace ApiTests.UnitTests.Deductions
{
    public class BaseBenefitDeductionTests
    {
        private readonly BaseBenefitDeduction _subject;

        public BaseBenefitDeductionTests()
        {
            _subject = new BaseBenefitDeduction();
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
            decimal expectedDeductionAmount = 461.54m;
            
            // ACT
            var deductionAmount = _subject.CalculatePaycheckDeduction(new Employee(), 26);

            // MEASURE
            Assert.Equal(expectedDeductionAmount, deductionAmount);
        }
    }
}
