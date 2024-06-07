using InnovaWideTest.Application.Services.AuthServices;
using InnovaWideTest.Domain.DTOs.Authe;
using Microsoft.AspNetCore.Mvc;

namespace InnovaWideTest.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UsersController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<AutheDto>> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Login(model);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<AutheDto>> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Register(model);
            return result;
        }
    }
}
