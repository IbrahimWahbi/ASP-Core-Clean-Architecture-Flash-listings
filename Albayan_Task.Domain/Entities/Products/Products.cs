using Albayan_Task.Domain.Entities.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albayan_Task.Domain.Entities.Products
{
    public class Products: BaseEntity
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }

        [Column(TypeName = "bigint")]
        public double Duration { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public long CategoryID { get; set; }
        public ICollection<ProductCustomField> ProductCustomFields { get; set; }
    }
}
