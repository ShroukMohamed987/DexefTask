using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexefTask.BL.DTOS.Identity
{
    public class RegisterUserDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        [Required]
        [DefaultValue(5000)]
        public int Salary { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Address { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        [DefaultValue("Developer")]
        public string JobTitle { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        
        public DateTime CreatedAt { get; set; }= DateTime.Now;
    }
}
