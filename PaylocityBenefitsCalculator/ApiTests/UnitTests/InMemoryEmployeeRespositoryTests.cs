using Api.Models;
using Api.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ApiTests.UnitTests
{
    public class InMemoryEmployeeRespositoryTests
    {
        private readonly InMemoryEmployeeRepository _subject;

        public InMemoryEmployeeRespositoryTests()
        {
            _subject = new InMemoryEmployeeRepository();
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmployees()
        {
            // ARRANGE
            var expectedEmployees = new List<Employee>(3)
            {
                new()
                {
                    Id = 1,
                    FirstName = "LeBron",
                    LastName = "James",
                    Salary = 75420.99m,
                    DateOfBirth = new DateTime(1984, 12, 30)
                },
                new()
                {
                    Id = 2,
                    FirstName = "Ja",
                    LastName = "Morant",
                    Salary = 92365.22m,
                    DateOfBirth = new DateTime(1999, 8, 10),
                    Dependents = new List<Dependent>
                    {
                        new()
                        {
                            Id = 1,
                            FirstName = "Spouse",
                            LastName = "Morant",
                            Relationship = Relationship.Spouse,
                            DateOfBirth = new DateTime(1998, 3, 3)
                        },
                        new()
                        {
                            Id = 2,
                            FirstName = "Child1",
                            LastName = "Morant",
                            Relationship = Relationship.Child,
                            DateOfBirth = new DateTime(2020, 6, 23)
                        },
                        new()
                        {
                            Id = 3,
                            FirstName = "Child2",
                            LastName = "Morant",
                            Relationship = Relationship.Child,
                            DateOfBirth = new DateTime(2021, 5, 18)
                        }
                    }
                },
                new()
                {
                    Id = 3,
                    FirstName = "Michael",
                    LastName = "Jordan",
                    Salary = 143211.12m,
                    DateOfBirth = new DateTime(1963, 2, 17),
                    Dependents = new List<Dependent>
                    {
                        new()
                        {
                            Id = 4,
                            FirstName = "DP",
                            LastName = "Jordan",
                            Relationship = Relationship.DomesticPartner,
                            DateOfBirth = new DateTime(1974, 1, 2)
                        }
                    }
                }
            };

            // ACT
            var employees = await _subject.GetAll();

            // MEASURE
            Assert.NotNull(employees);
            // NOTE: Upgraded minor version of XUnit to get access to this
            Assert.Equivalent(expectedEmployees, employees, true);
        }

        [Fact]
        public async Task GetById_ShouldReturnMatchingEmployee()
        {
            // ARRANGE
            var expectedEmployee = new Employee()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = new List<Dependent>
                    {
                        new()
                        {
                            Id = 4,
                            FirstName = "DP",
                            LastName = "Jordan",
                            Relationship = Relationship.DomesticPartner,
                            DateOfBirth = new DateTime(1974, 1, 2)
                        }
                    }
            };

            // ACT
            var employee = await _subject.GetById(expectedEmployee.Id);

            // MEASURE
            Assert.NotNull(employee);
            Assert.True(expectedEmployee.Equals(employee));
        }

        [Fact]
        public async Task GetById_ShouldReturnNullIfNotFound()
        {
            // ARRANGE
            var missingDependentId = int.MinValue;

            // ACT
            var dependent = await _subject.GetById(missingDependentId);

            // MEASURE
            Assert.Null(dependent);
        }
    }
}
