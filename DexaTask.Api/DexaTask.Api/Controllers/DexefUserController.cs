using DexefTask.BL.DTOS;
using DexefTask.BL.DTOS.Identity;
using DexefTask.BL.IReposatories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DexefTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DexefUserController : Controller
    {
        private readonly IDexefUserRepo _dexefUserRepo;

        public DexefUserController(IDexefUserRepo dexefUserRepo)
        {
            _dexefUserRepo = dexefUserRepo;
        }

        [HttpGet]
        public async Task<ActionResult<ReadDexefUserDto>>  GetUserById(Guid id)
        {
           return Ok( await _dexefUserRepo.GetById(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _dexefUserRepo.DeleteDexefUser(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> AddDexefUser([FromForm]RegisterUserDto dexefUser)
        {
            if(dexefUser == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _dexefUserRepo.RegisterDexefUser(dexefUser);
            return Ok(dexefUser);
        }

        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(typeof(UpdateDexefUserDto), 200)]

        public async Task<ActionResult> UpdateDexefUser(Guid id ,[FromForm] UpdateDexefUserDto dexefUser)
        {
            await _dexefUserRepo.UpdateDexefUser(dexefUser, id);
            return NoContent();
        }

        [HttpGet]
      [Route("GetAllDexefUser")]
        public async Task<ActionResult<IEnumerable<ReadDexefUserDto>>> GetPagedList([FromQuery]int? pageNumber,[FromQuery] string? SearchedName = null)
        {
           return Ok( await _dexefUserRepo.PagedList(pageNumber, SearchedName));
        }
    }
}
