using Api.BusinessLogic;
using Api.Models;
using System;
using Xunit;

namespace ApiTests.UnitTests.Deductions
{
    public class OlderDependentsDeductionTests
    {
        private readonly OlderDependentsDeduction _subject;

        public OlderDependentsDeductionTests()
        {
            _subject = new OlderDependentsDeduction();
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
            decimal expectedDeductionAmount = 184.62m;

            // ARRANGE
            var employee = new Employee()
            {
                Id = 1,
                Dependents = new[]
                {
                    new Dependent()
                    {
                        DateOfBirth = DateTime.Now.AddYears(-14),
                    },
                    new Dependent()
                    {
                        DateOfBirth = DateTime.Now.AddYears(-61),
                    },
                    new Dependent()
                    {
                        DateOfBirth = DateTime.Now.AddYears(-63),
                    }
                }
            };

            // ACT
            var deductionAmount = _subject.CalculatePaycheckDeduction(employee, 26);

            // MEASURE
            Assert.Equal(expectedDeductionAmount, deductionAmount);
        }

        [Fact]
        public void CalculatePaycheckDeduction_ZeroOlderDependents_ReturnsZeroDeduction()
        {
            decimal expectedDeductionAmount = 0.00m;

            // ARRANGE
            var employee = new Employee()
            {
                Dependents = new[]
                {
                    new Dependent()
                    { 
                        DateOfBirth = DateTime.Now.AddYears(-7),
                    },
                    new Dependent(){
                        DateOfBirth = DateTime.Now.AddYears(-12),
                    },
                    new Dependent()
                    {
                        DateOfBirth = DateTime.Now.AddYears(-49),
                    }
                }
            };

            // ACT
            var deductionAmount = _subject.CalculatePaycheckDeduction(employee, 26);

            // MEASURE
            Assert.Equal(expectedDeductionAmount, deductionAmount);
        }
    }
}
