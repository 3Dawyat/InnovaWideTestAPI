using InnovaWideTest.Domain.DTOs.Authe;

namespace InnovaWideTest.Application.Services.AuthServices
{
    public interface IAuthService
    {
        Task<AutheDto> Login(LoginDto model);
        Task<AutheDto> Register(RegisterDto model);
    }
}
