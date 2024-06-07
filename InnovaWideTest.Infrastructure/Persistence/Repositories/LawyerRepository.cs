using InnovaWideTest.Application.Common.Interfaces.Repositories;
using InnovaWideTest.Domain.Entities;

namespace InnovaWideTest.Infrastructure.Persistence.Repositories
{
    public class LawyerRepository : BaseRepository<Lawyer>, ILawyerRepository
    {
        public LawyerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
