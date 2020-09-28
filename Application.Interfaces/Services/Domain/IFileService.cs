using Application.Interfaces.Services.Standard;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Application.Interfaces.Services.Domain
{
    public interface IFileService : IServiceBase<File>
    {
        Task<IEnumerable<File>> GetAllIncludingTasksAsync();
        Task<File> GetByIdIncludingTasksAsync(int id);
    }
}
