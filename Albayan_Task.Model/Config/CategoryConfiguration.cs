using Albayan_Task.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Albayan_Task.Domain.Entities.Categories;

namespace Albayan_Task.Model.Config
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.ArabicName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.EnglishName).IsRequired().HasMaxLength(100);
            builder.HasMany(s => s.Products).WithOne(s => s.Category).HasForeignKey(s => s.CategoryID);
            
        }
    }
}
