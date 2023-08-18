using Api.Interfaces;
using Api.Models;

namespace Api.Repositories
{
    // why: Assumed persisted storage was not neccessary for this exercise. The
    //  use of an interface for this allows easy creation of a new way of storing this data
    //  through the implementation of a new class that implements the interface.
    public class InMemoryDependentRepository : IDependentRepository
    {
        private readonly List<Dependent> _dependents;

        public InMemoryDependentRepository()
        {
            _dependents = new List<Dependent>()
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
        }

        public async Task<IEnumerable<Dependent>> GetAll()
        {
            return await Task.FromResult<IEnumerable<Dependent>>(_dependents.ToArray());
        }

        public async Task<Dependent?> GetById(int id)
        {
            return await Task.Run<Dependent?>( () => {
                return _dependents.FirstOrDefault(d => d.Id == id);
            });
        }
    }
}
