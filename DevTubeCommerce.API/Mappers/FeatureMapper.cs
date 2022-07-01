using AutoMapper;
using DevTubeCommerce.API.Forms.Catalogs.Features;
using DevTubeCommerce.API.OutputModels.Catalogs.Features;
using DevTubeCommerce.Application.Contract.Dto.Catalog;

namespace DevTubeCommerce.API.Mappers
{
    public class FeatureMapper : Profile
    {
        public FeatureMapper()
        {
            CreateMap<AddFeatureForm, FeatureDto>();
            CreateMap<UpdateFeatureForm, FeatureDto>();
            CreateMap<FeatureDto, FeatureModel>();
        }
    }
}
