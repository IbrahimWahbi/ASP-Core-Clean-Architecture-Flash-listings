using Albayan_Task.Domain.Entities.Categories;
using Albayan_Task.Domain.Entities.Products;
using Albayan_Task.Domain.Interfaces;
using Albayan_Task.DTO.Categories;
using Albayan_Task.DTO.Products;
using Albayan_Task.Service.Iservices.Icategories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albayan_Task.Service.Services.Categories
{
    public class CategoriesService: ICategoriesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task< bool> DeleteCategory(long catid)
        {

            _unitOfWork.repository<Category>().Delete(await _unitOfWork.repository<Category>().GetByIdAsync(catid));
            var res =  await _unitOfWork.Complete();
            return true;
        }

        public async Task<List<DTOCategory>> GetCategories(string lang ="en")
        {
            return _mapper.Map<IEnumerable<Category>, List<DTOCategory>> (await _unitOfWork.repository<Category>().GetAllAsync(), opt => opt.Items["lang"] = lang);
        }

        public async Task<bool> isAvilable(long catId)
        {
            var res = await _unitOfWork.repository<Category>().GetByIdAsync(catId);
            return res == null ? false : true;
        }

        public async Task<List<DTOProducts>> GetCategoryProduct(long catid,string lang = "en")
        {
            return _mapper.Map<IEnumerable<Products>, List<DTOProducts>>(await _unitOfWork.repository<Products>().Get(s=>s.CategoryID == catid, includeProperties: "Category,ProductCustomFields,ProductCustomFields.CustomField"), opt => opt.Items["lang"] = lang);
        }
    }
}
