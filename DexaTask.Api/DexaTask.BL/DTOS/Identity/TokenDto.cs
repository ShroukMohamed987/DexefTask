using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexefTask.BL.DTOS.Identity
{
    public record TokenDto
    (string token, DateTime Expire);

}
