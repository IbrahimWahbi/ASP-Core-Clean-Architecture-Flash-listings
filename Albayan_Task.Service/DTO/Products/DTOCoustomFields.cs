using Albayan_Task.Domain.Entities;
using Albayan_Task.Domain.Entities.Products;

namespace Albayan_Task.DTO.Products
{
    public class DTOCustomFields:BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public long ProductCustomFieldId { get; set; }
    }
}
