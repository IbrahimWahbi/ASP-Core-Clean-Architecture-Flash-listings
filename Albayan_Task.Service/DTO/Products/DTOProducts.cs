using Albayan_Task.Domain.Entities;
using Albayan_Task.Domain.Entities.Categories;
using Albayan_Task.Domain.Entities.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace Albayan_Task.DTO.Products
{
    public class DTOProducts : BaseEntity
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public long CategoryID { get; set; }
        public ICollection<DTOProductCoustomField> ProductCustomFields { get; set; }
    }
}
