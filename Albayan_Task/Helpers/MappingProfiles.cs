using Albayan_Task.Domain.Entities.Categories;
using Albayan_Task.Domain.Entities.Products;
using Albayan_Task.DTO.Categories;
using Albayan_Task.DTO.Products;
using AutoMapper;

namespace Albayan_Task.Helpers
{
    public class MappingProfiles : Profile
    {
        // init Auto mapper basicly we us it for loclization
        public MappingProfiles()
        {
            CreateMap<Category, DTOCategory>().ForMember
                (s => s.Name, opt => opt.MapFrom
                ((src, dest, destMember, context) => (string)context.Items["lang"] == "ar" ? src.ArabicName : src.EnglishName));

            CreateMap<Products, DTOProducts>()
                .ForMember(s=>s.Duration , opt => opt.MapFrom(s=>TimeSpan.FromHours( s.Duration)))
                .ForMember
                (s => s.Name, opt => opt.MapFrom
                ((src, dest, destMember, context) => (string)context.Items["lang"] == "ar" ? src.ArabicName : src.EnglishName))
                .ForMember
               (s => s.Category, opt => opt.MapFrom
               ((src, dest, destMember, context) => ((string)context.Items["lang"] == "ar" ? src.Category.ArabicName : src.Category.EnglishName)));

            CreateMap<ProductCustomField, DTOProductCoustomField>().ForMember
                (s => s.Title, opt => opt.MapFrom
                ((src, dest, destMember, context) => ((string)context.Items["lang"] == "ar" ? src.ArabicTitle : src.EnglishTitle)));

            CreateMap<CustomFields, DTOCustomFields>().ForMember
               (s => s.Value, opt => opt.MapFrom
               ((src, dest, destMember, context) => ((string)context.Items["lang"] == "ar" ? src.ArabicValue : src.EnglishValue)))
               .ForMember
               (s => s.Key, opt => opt.MapFrom
               ((src, dest, destMember, context) => ((string)context.Items["lang"] == "ar" ? src.ArabicKey : src.EnglishKey)));

            CreateMap<DtoAddProduct, Products>().ForMember(s=>s.Duration ,opt=>opt.MapFrom(s=> s.DurationInHours));
            CreateMap<DTOAddCustomFields, CustomFields>();
            CreateMap<DTOAddProductCustomField, ProductCustomField>();
        }
    }
}
