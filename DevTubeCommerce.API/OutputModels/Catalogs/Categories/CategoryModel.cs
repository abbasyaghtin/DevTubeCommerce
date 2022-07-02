using DevTubeCommerce.API.OutputModels.Catalogs.Features;

namespace DevTubeCommerce.API.OutputModels.Catalogs.Categories
{
    public record CategoryModel
    {
        public Guid Id { get; init; }
        public string CategoryName { get; init; }
        public bool IsActive { get; init; }
        public string Description { get; init; }
        public List<FeatureModel> Features { get; set; }
    }
}
