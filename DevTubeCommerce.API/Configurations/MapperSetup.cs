using AutoMapper;
using DevTubeCommerce.API.Mappers;
using DevTubeCommerce.Application.Contract.Interfaces.Catalog;

namespace DevTubeCommerce.API.Configurations
{
    public static class MapperSetup
    {
        public static void AddMapperSetup(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
        }
    }
}
