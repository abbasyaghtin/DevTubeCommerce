namespace DevTubeCommerce.Domain.Core.Catalogs.Features
{
    public interface IFeatureRepository
    {
        Task<FeatureId> Insert(Feature feature);
        Task Update(Feature feature);
        Task<Feature> GetById(FeatureId featureId);
        Task<Feature> GetByIdAsync(FeatureId featureId, CancellationToken cancellationToken = default);
        Task<List<Feature>> GetFeaturesAsync(CancellationToken cancellationToken = default);
        void Delete(FeatureId featureId);
        Task<List<Feature>> GetByIdsAsync(List< FeatureId> featureIds, CancellationToken cancellationToken);
    }
}
