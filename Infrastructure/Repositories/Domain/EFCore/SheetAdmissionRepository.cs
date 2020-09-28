using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Repositories.Standard.EFCore;

namespace Infrastructure.Repositories.Domain.EFCore
{
    public class SheetAdmissionRepository : DomainRepository<SheetAdmission>,
                           ISheetAdmissionRepository
    {
        public SheetAdmissionRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<SheetAdmission>> GetAllIncludingTasksAsync()
        {
            IQueryable<SheetAdmission> query = await Task.FromResult(GenerateQuery(filter: null,
                                                                         orderBy: null));
            return query.AsEnumerable();
        }

        public async Task<SheetAdmission> GetByIdIncludingTasksAsync(int id)
        {
            IQueryable<SheetAdmission> query = await Task.FromResult(GenerateQuery(filter: (user => user.Id == id),
                                                                         orderBy: null));
      
            return query.SingleOrDefault();
        }

        Task<IEnumerable<SheetAdmission>> ISheetAdmissionRepository.GetAllIncludingTasksAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
