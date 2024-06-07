using InnovaWideTest.Application.Services.TenantServices;
using InnovaWideTest.Domain.Entities;
using InnovaWideTest.Domain.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InnovaWideTest.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public string TenantId { get; set; }
        private readonly ITenantService _tenantService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantService tenantService)
            : base(options)
        {
            _tenantService = tenantService;
            TenantId = _tenantService.GetCurrentTenant();
        }


        public DbSet<Lawyer> Lawyers { get; set; }
        public DbSet<Hearing> Hearings { get; set; }
        public DbSet<Case> Cases { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Lawyer>().HasQueryFilter(e => e.TenantId == TenantId);
            modelBuilder.Entity<Hearing>().HasQueryFilter(e => e.TenantId == TenantId);
            modelBuilder.Entity<Case>().HasQueryFilter(e => e.TenantId == TenantId);

            modelBuilder.Ignore<IdentityRole>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityRoleClaim<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().Where(e => e.State == EntityState.Added))
            {
                if (string.IsNullOrEmpty(entry.Entity.TenantId))
                    entry.Entity.TenantId = TenantId;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
