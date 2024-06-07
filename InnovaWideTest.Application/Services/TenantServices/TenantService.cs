using Microsoft.AspNetCore.Http;

namespace InnovaWideTest.Application.Services.TenantServices
{
    public class TenantService : ITenantService
    {
        private string _currentTenant;

        public TenantService(IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor.HttpContext is not null)
            {
                if (contextAccessor.HttpContext.Request.Headers.TryGetValue("Tenant", out var tenantId))
                {
                    _currentTenant = tenantId!;
                }
                else
                {
                    if (!contextAccessor.HttpContext.Request.Path.Value.Contains("Users"))
                        throw new Exception("No Tenant !");
                }
            }
        }
        public string GetCurrentTenant()
        {
            return _currentTenant;
        }

    }
}
