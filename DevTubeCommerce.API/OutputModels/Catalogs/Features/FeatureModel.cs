namespace DevTubeCommerce.API.OutputModels.Catalogs.Features
{
    public record FeatureModel
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public int SortOrder { get; init; }
    }
}
