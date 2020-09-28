using Application.Interfaces.Admission.Imports;
using Application.Services.Standard;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Admission.Imports
{
    public class ImportsService: ServiceBase<SheetAdmission>,
                          IImportsService
    {
        private readonly IImportsRepository _repository;

        public ImportsService(IImportsRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public List<ReportFileImports> GetAllImports()
        {
            return _repository.GetAllImports();
        }

        public Task<List<SheetAdmission>> GetImportById(int id)
        {
            return _repository.GetImportById(id);
        }
    
    }
}
