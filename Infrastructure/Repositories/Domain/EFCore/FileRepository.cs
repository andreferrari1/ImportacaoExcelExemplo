using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Repositories.Standard.EFCore;

namespace Infrastructure.Repositories.Domain.EFCore
{
    public class FileRepository : DomainRepository<File>,
                                  IFileRepository
    {
        public FileRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<File>> GetAllIncludingTasksAsync()
        {
            IQueryable<File> query = await Task.FromResult(GenerateQuery(filter: null,
                                                                         orderBy: null));
            return query.AsEnumerable();
        }

        public async Task<File> GetByIdIncludingTasksAsync(int id)
        {
            IQueryable<File> query = await Task.FromResult(GenerateQuery(filter: (user => user.Id == id),
                                                                         orderBy: null));
            return query.SingleOrDefault();
        }
    }
}