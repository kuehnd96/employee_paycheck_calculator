using Api.BusinessLogic;
using Api.Interfaces;
using Api.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace ApiTests.UnitTests.Deductions
{
    public class BiweeklyPaycheckCalculatorTests
    {
        private readonly BiweeklyPaycheckCalculator _subject;

        public BiweeklyPaycheckCalculatorTests()
        {
            var deductions = new List<IDeduction>()
            {
                new BaseBenefitDeduction(),
                new DependentBenefitDeduction(),
                new HighSalaryDeduction(),
                new OlderDependentsDeduction()
            };
            
            _subject = new BiweeklyPaycheckCalculator(deductions);
        }

        [Fact]
        public void CalculatePaycheckAmount_NullEmployee_ExceptionThrown()
        {
            // ACT & MEASURE
            Assert.Throws<ArgumentNullException>(() => 
                _subject.CalculatePaycheckAmount(null));
        }

        // Note: I have used InlineData but I hadn't used MemberData before.
        // I like this but I wish each run of this test was shown in the test explorer, though.
        [Theory, MemberData(nameof(EmployeeData))]
        public void CalculatePaycheckAmount_ReturnsExpectedAmount(Employee employee, decimal expectedAmount)
        {
            // ACT
            var paycheckAmount = _subject.CalculatePaycheckAmount(employee);

            // MEASURE
            Assert.Equal(expectedAmount, paycheckAmount);
        }

        public static IEnumerable<object[]> EmployeeData =>
        new List<object[]>
        {
            new object[] 
            {   
                new Employee() // basic with no dependents 
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Smith",
                    Salary = 63000m,
                    DateOfBirth = DateTime.Now.AddYears(-34).AddMonths(-4)
                }, 
                1961.54m 
            },
            new object[] 
            {
                new Employee() // child dependents only 
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Salary = 78000m,
                    DateOfBirth = DateTime.Now.AddYears(-42).AddMonths(-7),
                    Dependents = new List<Dependent>()
                    {
                        new()
                        {
                            Id = 1,
                            FirstName = "Sebastian",
                            LastName = "Doe",
                            Relationship = Relationship.Child,
                            DateOfBirth = new DateTime(2011, 3, 13),
                            EmployeeId = 2
                        },
                        new()
                        {
                            Id = 2,
                            FirstName = "Harrison",
                            LastName = "Doe",
                            Relationship = Relationship.Child,
                            DateOfBirth = new DateTime(2014, 6, 23),
                            EmployeeId = 2
                        },
                        new()
                        {
                            Id = 3,
                            FirstName = "Sawyer",
                            LastName = "Doe",
                            Relationship = Relationship.Child,
                            DateOfBirth = new DateTime(2018, 5, 18),
                            EmployeeId = 2
                        }
                    }
                },
                1707.69m
            },
            new object[]
            {
                new Employee() // all deductions triggered 
                {
                    Id = 3,
                    FirstName = "Jack",
                    LastName = "Conner",
                    Salary = 92000m,
                    DateOfBirth = DateTime.Now.AddYears(-55),
                    Dependents = new List<Dependent>()
                    {
                        new()
                        {
                            Id = 1,
                            FirstName = "Gwendolyn",
                            LastName = "Connor",
                            Relationship = Relationship.Spouse,
                            DateOfBirth = DateTime.Now.AddYears(-54),
                            EmployeeId = 3
                        },
                        new()
                        {
                            Id = 2,
                            FirstName = "Steven",
                            LastName = "Connor",
                            Relationship = Relationship.Child,
                            DateOfBirth = new DateTime(2016, 8, 23),
                            EmployeeId = 3
                        },
                        new()
                        {
                            Id = 3,
                            FirstName = "Vicki",
                            LastName = "Connor",
                            Relationship = Relationship.Child,
                            DateOfBirth = new DateTime(2019, 2, 18),
                            EmployeeId = 3
                        }
                    }
                },
                2083.07m
            },
        };
    }

    
}
