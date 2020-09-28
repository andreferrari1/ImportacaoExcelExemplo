using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain.Standard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories.Domain
{
    public interface ISheetAdmissionRepository : IDomainRepository<SheetAdmission>
    {
        Task<IEnumerable<SheetAdmission>> GetAllIncludingTasksAsync();
        Task<SheetAdmission> GetByIdIncludingTasksAsync(int id);
    }
}