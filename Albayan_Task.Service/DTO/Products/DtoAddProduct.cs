using Albayan_Task.Domain.Entities.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albayan_Task.Domain.Entities.Products
{
    public class DtoAddProduct : BaseEntity
    {
        [Required(ErrorMessage = "The Arabic name is required")]
        [MaxLength(100)]
        public string ArabicName { get; set; }
        [Required(ErrorMessage = "The English name is required")]
        [MaxLength(100)]
        public string EnglishName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        [Range(1, 10000)]
        public double DurationInHours { get; set; }
        public decimal Price { get; set; }
        [Required(ErrorMessage = "The Category is reqired")]
        public long CategoryID { get; set; }
        public List<DTOAddProductCustomField> ProductCustomFields { get; set; }
    }
}
