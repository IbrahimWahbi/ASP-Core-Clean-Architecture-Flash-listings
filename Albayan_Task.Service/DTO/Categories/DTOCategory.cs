using Albayan_Task.Domain.Entities;
using Albayan_Task.DTO.Products;
using System.ComponentModel.DataAnnotations;

namespace Albayan_Task.DTO.Categories
{
    public class DTOCategory:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<DTOProducts> Products { get; set; }
    }
}
