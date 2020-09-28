using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces.Services.Domain;
using Application.Services.Standard;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain;

namespace Application.Services.Domain
{
    public class SheetAdmissionService : ServiceBase<SheetAdmission>,
                              ISheetAdmissionService
    {
        private readonly ISheetAdmissionRepository _repository;

        public SheetAdmissionService(ISheetAdmissionRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SheetAdmission>> GetAllIncludingTasksAsync()
        {
            return await _repository.GetAllIncludingTasksAsync();
        }

        public async Task<SheetAdmission> GetByIdIncludingTasksAsync(int id)
        {
            return await _repository.GetByIdIncludingTasksAsync(id);
        }
    }
}