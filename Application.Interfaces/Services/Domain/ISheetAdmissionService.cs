using Application.Interfaces.Services.Standard;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Domain
{
   public interface ISheetAdmissionService : IServiceBase<SheetAdmission>
    {
        Task<IEnumerable<SheetAdmission>> GetAllIncludingTasksAsync();
        Task<SheetAdmission> GetByIdIncludingTasksAsync(int id);
    }
}