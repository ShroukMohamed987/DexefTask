using DexefTask.DAL.Configurations;

using DexefTask.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexefTask.DAL.Context
{
    public class ApplicationDbContext :IdentityDbContext<DexefUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            DexefUserConfig config = new DexefUserConfig();

            config.Configure(builder.Entity<DexefUser>());

            builder.ApplyConfiguration(config);

            base.OnModelCreating(builder);
        }
    }
}
