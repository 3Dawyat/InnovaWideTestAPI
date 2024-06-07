using InnovaWideTest.Domain.Helper;
using Microsoft.AspNetCore.Identity;

namespace InnovaWideTest.Domain.Entities
{
    public class ApplicationUser : IdentityUser, IMustHaveTenant
    {
        public string Name { get; set; }
        public string? TenantId { get; set; }
    }

}
