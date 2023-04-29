using Albayan_Task.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Albayan_Task.Domain.Entities.Categories
{
    public class Category:BaseEntity
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public ICollection<Products.Products> Products { get; set; }
    }
}
