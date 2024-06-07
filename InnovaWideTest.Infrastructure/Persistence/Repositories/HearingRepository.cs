using InnovaWideTest.Application.Common.Interfaces.Repositories;
using InnovaWideTest.Domain.Entities;

namespace InnovaWideTest.Infrastructure.Persistence.Repositories
{
    public class HearingRepository : BaseRepository<Hearing>, IHearingRepository
    {
        public HearingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
