using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain.Standard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories.Domain
{
    public interface IFileRepository : IDomainRepository<File>
    {
        Task<IEnumerable<File>> GetAllIncludingTasksAsync();
        Task<File> GetByIdIncludingTasksAsync(int id);
    }
}
