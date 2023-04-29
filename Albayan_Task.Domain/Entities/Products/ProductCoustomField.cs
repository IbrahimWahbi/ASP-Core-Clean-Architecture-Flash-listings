﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albayan_Task.Domain.Entities.Products
{
    public class ProductCustomField:BaseEntity
    {
        public string ArabicTitle { get; set; }
        public string EnglishTitle { get; set; }
        public  ICollection< CustomFields> CustomField { get; set; }
        public long ProductId { get; set; }
        public Products Product { get; set; }
    }
}
