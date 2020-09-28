using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces.Services.Domain;
using Application.Services.Standard;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain;

namespace Application.Services.Domain
{
   public class FileService : ServiceBase<File>,
                           IFileService
    {
        private readonly IFileRepository _repository;

        public FileService(IFileRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<File>> GetAllIncludingTasksAsync()
        {
            return await _repository.GetAllIncludingTasksAsync();
        }

        public async Task<File> GetByIdIncludingTasksAsync(int id)
        {
            return await _repository.GetByIdIncludingTasksAsync(id);
        }
    }
}