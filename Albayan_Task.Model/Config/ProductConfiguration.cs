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
    internal class ProductConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.ArabicName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.EnglishName).IsRequired().HasMaxLength(100);
            // we shoudld use cascade delete here 
            builder.HasMany(s => s.ProductCustomFields).WithOne(s => s.Product).HasForeignKey(s => s.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
