using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain.Standard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories.Domain
{
    public interface IImportsRepository : IDomainRepository<SheetAdmission>
    {
        List<ReportFileImports> GetAllImports();
        Task<List<SheetAdmission>> GetImportById(int id);
    }
}