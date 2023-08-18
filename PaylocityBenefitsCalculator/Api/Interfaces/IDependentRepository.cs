using Api.Models;

namespace Api.Interfaces
{
    /// <summary>
    /// Surface for interacting with <see cref="Dependent"/> persisted data.
    /// </summary>
    public interface IDependentRepository
    {
        // why: Methods return tasks to facilitate implemenations of this interface that are async
        
        Task<IEnumerable<Dependent>> GetAll();
        Task<Dependent?> GetById(int id);
    }
}
