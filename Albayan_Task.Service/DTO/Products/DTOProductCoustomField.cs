using Albayan_Task.Domain.Entities;
using Albayan_Task.Domain.Entities.Products;

namespace Albayan_Task.DTO.Products
{
    public class DTOProductCoustomField : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<DTOCustomFields> CustomField { get; set; }
        public long ProductId { get; set; }
    }
}
