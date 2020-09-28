using System;
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
    public class ImportsRepository: DomainRepository<SheetAdmission>, IImportsRepository
    {
        public ImportsRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public List<ReportFileImports> GetAllImports()
        {
            return (from q in
                                (from p in dbSet
                                 select new
                                 {
                                     p.FileId,
                                     p.Amount,
                                     p.DeliveryDate,
                                     p.File.DateRegister,
                                     p.UnitaryValue
                                 })
                    group q by q.FileId into g
                    orderby g.Key
                    select new ReportFileImports
                    {
                        FileId = g.Key,
                        FileDateRegister = g.Max(p => p.DateRegister),
                        Amount = g.Sum(p => p.Amount),
                        DeliveryDateMin = g.Min(p => p.DeliveryDate),
                        AmountValue = g.Sum(p => p.Amount * p.UnitaryValue)
                    }).ToList();
        }

        public async Task<List<SheetAdmission>> GetImportById(int id)
        {
            IQueryable<SheetAdmission> query = await Task.FromResult(GenerateQuery(filter: (file => file.FileId == id), orderBy: null));
            return query.ToList();
        }

    }
}
