using AutoMapper;
using DevTubeCommerce.API.Forms.Catalogs.Categories;
using DevTubeCommerce.API.OutputModels.Catalogs.Categories;
using DevTubeCommerce.API.OutputModels.Catalogs.Features;
using DevTubeCommerce.Application.Contract.Dto.Catalog;
using DevTubeCommerce.Application.Contract.Interfaces.Catalog;

namespace DevTubeCommerce.API.Mappers
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<AddCategoryForm, CategoryDto>();
            CreateMap<UpdateCategoryForm, CategoryDto>();
            CreateMap<CategoryDto, CategoryModel>();
        }

    }
}
