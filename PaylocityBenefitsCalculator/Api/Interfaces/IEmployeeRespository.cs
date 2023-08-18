using Api.Models;

namespace Api.Interfaces
{
    /// <summary>
    /// Surface for interacting with <see cref="Employee"/> persisted data.
    /// </summary>
    public interface IEmployeeRepository
    {
        // why: Methods return tasks to facilitate implemenations of this interface that are async

        Task<IEnumerable<Employee>> GetAll();
        Task<Employee?> GetById(int id);
    }
}
