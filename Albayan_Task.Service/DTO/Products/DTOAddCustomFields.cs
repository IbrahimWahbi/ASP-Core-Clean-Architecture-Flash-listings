using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albayan_Task.Domain.Entities.Products
{
    public class DTOAddCustomFields : BaseEntity
    {
        public string ArabicKey { get; set; }
        public string EnglishKey { get; set; }
        public string ArabicValue { get; set; }
        public string EnglishValue { get; set; }
        public long ProductCustomFieldId { get; set; }
    }
}
