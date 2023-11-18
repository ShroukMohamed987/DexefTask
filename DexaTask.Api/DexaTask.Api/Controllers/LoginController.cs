using DexefTask.BL.DTOS.Identity;
using DexefTask.BL.IReposatories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DexefTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginServices _loginServices;

        public LoginController(ILoginServices loginServices)
        {
            _loginServices = loginServices;
        }

        [HttpPost]
        public async Task<ActionResult<TokenDto>> Login(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest();

            }
            return await _loginServices.LoginAsync(loginDto);
        }
    }
}
