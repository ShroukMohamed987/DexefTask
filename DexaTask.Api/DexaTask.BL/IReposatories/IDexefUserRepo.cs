using DexefTask.BL.DTOS;
using DexefTask.BL.DTOS.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexefTask.BL.IReposatories
{
    public interface IDexefUserRepo
    {
        Task RegisterDexefUser(RegisterUserDto registerUserDto);

        Task<IEnumerable<ReadDexefUserDto>> PagedList(int? pageNumber, string? UserName);

        Task<ReadDexefUserDto> GetById(Guid id);
        Task<IEnumerable<ReadDexefUserDto>> GetAll();
        Task<IEnumerable<ReadDexefUserDto>> GetDexefUserForPage(int pageNumber);
        Task<IEnumerable<ReadDexefUserDto>> GetDexefUserByName(string UserName);
        Task<IEnumerable<ReadDexefUserDto>> GetDexefUserForPageWithName(int pageNumber, string UserName);

        Task UpdateDexefUser(UpdateDexefUserDto updateDexefUser, Guid id);

        Task DeleteDexefUser(Guid id);

        Task SaveChanges();

    }
}
