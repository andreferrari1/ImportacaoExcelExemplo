using Application.Interfaces.Services.Standard;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Admission.Imports
{
    public interface IImportsService : IServiceBase<SheetAdmission>
    {
        List<ReportFileImports> GetAllImports();
        Task<List<SheetAdmission>> GetImportById(int id);
    }
}