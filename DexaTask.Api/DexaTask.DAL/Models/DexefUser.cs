using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexefTask.DAL.Models
{
    public class DexefUser :IdentityUser
    {
        public int Salary { get; set; }
        public string Address { get; set; } 
        public string JobTitle { get; set; }

        public string Image { get; set; }

        public DateTime CreatedAt { get;set; }
        public DateTime UpdatedAt { get; set; }

    }
}
