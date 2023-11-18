using DexefTask.BL.DTOS.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexefTask.BL.IReposatories
{
    public interface ILoginServices
    {
        Task<TokenDto> LoginAsync(LoginDto loginDto);
    }
}
