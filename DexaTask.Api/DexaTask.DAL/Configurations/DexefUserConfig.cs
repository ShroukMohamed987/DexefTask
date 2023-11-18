using DexefTask.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexefTask.DAL.Configurations
{
    public class DexefUserConfig : IEntityTypeConfiguration<DexefUser>
    {
        public void Configure(EntityTypeBuilder<DexefUser> builder)
        {
            builder.Property(user => user.Address)
                   .IsRequired()
                   .HasDefaultValue(string.Empty)
                   .HasMaxLength(100);

            builder.Property(user => user.Salary)
                   .IsRequired()
                   .HasDefaultValue(5000);

            builder.Property(user => user.CreatedAt)
                   
                   .HasDefaultValueSql("GETUTCDATE()")
                   .ValueGeneratedOnAdd();

            //builder.Property(user => user.UpdatedAt)
                  
            //      .HasDefaultValueSql("GETDATE()");



            builder.Property(user => user.JobTitle)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasDefaultValue("Developer");

            builder.Property(user => user.Image)
                   .IsRequired()
                   .HasDefaultValue(string.Empty);
                   



        }
    }
}
