using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexefTask.BL.DTOS
{
    public class DexefUserDto
    {
        [Required]
        [DefaultValue(5000)]
        public int Salary { get; set; }

        [Required]
        public string UserName { get; set; } =string.Empty;

        [Required]
        [MaxLength(100)]
        public string Address { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        [DefaultValue("Developer")]
        public string JobTitle { get; set; }

        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
