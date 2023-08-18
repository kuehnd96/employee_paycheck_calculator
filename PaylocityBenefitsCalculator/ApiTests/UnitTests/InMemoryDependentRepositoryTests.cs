using Api.Models;
using Api.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ApiTests.UnitTests
{
    public class InMemoryDependentRepositoryTests
    {
        private readonly InMemoryDependentRepository _subject;

        public InMemoryDependentRepositoryTests()
        {
            _subject = new InMemoryDependentRepository();
        }

        [Fact]
        public async Task GetAll_ShouldReturnDependents()
        {
            // ARRANGE
            var expectedDependents = new List<Dependent>(4)
            {
                new()
                {
                    Id = 1,
                    FirstName = "Spouse",
                    LastName = "Morant",
                    Relationship = Relationship.Spouse,
                    DateOfBirth = new DateTime(1998, 3, 3),
                    EmployeeId = 2
                },
                new()
                {
                    Id = 2,
                    FirstName = "Child1",
                    LastName = "Morant",
                    Relationship = Relationship.Child,
                    DateOfBirth = new DateTime(2020, 6, 23),
                    EmployeeId = 2
                },
                new()
                {
                    Id = 3,
                    FirstName = "Child2",
                    LastName = "Morant",
                    Relationship = Relationship.Child,
                    DateOfBirth = new DateTime(2021, 5, 18),
                    EmployeeId = 2
                },
                new()
                {
                    Id = 4,
                    FirstName = "DP",
                    LastName = "Jordan",
                    Relationship = Relationship.DomesticPartner,
                    DateOfBirth = new DateTime(1974, 1, 2),
                    EmployeeId = 3
                }
            };

            // ACT
            var dependents = await _subject.GetAll();

            // MEASURE
            Assert.NotNull(dependents);
            // NOTE: Upgraded minor version of XUnit to get access to this
            Assert.Equivalent(expectedDependents, dependents, true);
        }

        [Fact]
        public async Task GetById_ShouldReturnMatchingDependent()
        {
            // ARRANGE
            var expectedDependent = new Dependent()
            {
                Id = 3,
                FirstName = "Child2",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2021, 5, 18),
                EmployeeId = 2
            };

            // ACT
            var dependent = await _subject.GetById(expectedDependent.Id);

            // MEASURE
            Assert.NotNull(dependent);
            Assert.True(expectedDependent.Equals(dependent));
        }

        [Fact]
        public async Task GetById_ShouldReturnNullIfNotFound()
        {
            // ARRANGE
            var missingDependentId = 67;

            // ACT
            var dependent = await _subject.GetById(missingDependentId);

            // MEASURE
            Assert.Null(dependent);
        }
    }
}
