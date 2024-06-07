using InnovaWideTest.Application.Common.Interfaces.Repositories;
using InnovaWideTest.Domain.Entities;

namespace InnovaWideTest.Infrastructure.Persistence.Repositories
{
    public class CaseRepository : BaseRepository<Case>, ICaseRepository
    {
        public CaseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
