using Albayan_Task.Domain.Entities.Categories;
using Albayan_Task.Domain.Entities.Products;
using Albayan_Task.Domain.Interfaces;
using Albayan_Task.DTO.Categories;
using Albayan_Task.DTO.Products;
using Albayan_Task.Service.Iservices.Iproducts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Albayan_Task.Service.Services.Categories
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<long> AddProduct(DtoAddProduct product)
        {
            var item =  _mapper.Map<DtoAddProduct, Products>(product);
            _unitOfWork.repository<Products>().Add(item);
            await _unitOfWork.Complete();
            return item.Id;
        }

        public async Task<bool> DeleteProduct(long ProductId)
        {
            _unitOfWork.repository<Products>().Delete(await _unitOfWork.repository<Products>().GetByIdAsync(ProductId));
            var res = await _unitOfWork.Complete();
            return true;
        }

        public async Task<bool> EditProduct(DtoAddProduct product)
        {
            _unitOfWork.repository<Products>().Update(_mapper.Map<DtoAddProduct, Products>(product));
            var res = await _unitOfWork.Complete();
            return true;
        }

        public DTOProducts GetProductById(long productId, string lang = "en")
        {
            
           var res =  _unitOfWork.repository<Products>().GetByIdIncludeAsync(productId, includeProperties: "Category,ProductCustomFields,ProductCustomFields.CustomField");
           return _mapper.Map<Products, DTOProducts>(res, opt => opt.Items["lang"] = lang);
        }

        public async Task<List<DTOProducts>> GetProducts(string lang = "en" )
        {
            var res = await _unitOfWork.repository<Products>().Get(s => s.StartDate.AddHours(s.Duration) >DateTime.Now,includeProperties: "Category,ProductCustomFields,ProductCustomFields.CustomField");
            
            return _mapper.Map<IEnumerable<Products>,List<DTOProducts>> (res, opt => opt.Items["lang"] = lang);
        }
    }
}
