using Albayan_Task.DTO.Categories;
using Albayan_Task.DTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albayan_Task.Service.Iservices.Icategories
{
    public interface ICategoriesService
    {
        public Task<List<DTOCategory>> GetCategories(string lang = "en");
        public Task<bool> isAvilable(long catId);
        public Task<bool> DeleteCategory(long catid);
        public Task<List<DTOProducts>> GetCategoryProduct(long catid,string lang = "en");
    }
}
