using AutoMapper;
using DevTubeCommerce.API.Forms.Catalogs.Products;
using DevTubeCommerce.Application.Contract.Commands.Products;
using DevTubeCommerce.Application.Contract.Dto.Product;

namespace DevTubeCommerce.API.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<AddProductForm, AddProductCommand>();              
            CreateMap<UpdateProductForm, UpdateProductCommand>();              
            CreateMap<ProductFeatureForm, ProductFeatureDto>();
        }
    }
}
