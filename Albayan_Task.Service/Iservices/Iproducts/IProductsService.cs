using Albayan_Task.Domain.Entities.Products;
using Albayan_Task.DTO.Categories;
using Albayan_Task.DTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albayan_Task.Service.Iservices.Iproducts
{
    public interface IProductsService
    {
        public Task<List<DTOProducts>> GetProducts(string lang = "en");
        public DTOProducts GetProductById(long productId, string lang = "en");
        public Task<long> AddProduct(DtoAddProduct product);
        public Task<bool> EditProduct(DtoAddProduct product);
        public Task<bool> DeleteProduct(long ProductId);
    }
}
