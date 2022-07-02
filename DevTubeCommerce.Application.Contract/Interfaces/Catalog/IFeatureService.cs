using DevTubeCommerce.Application.Contract.Dto.Catalog;

namespace DevTubeCommerce.Application.Contract.Interfaces.Catalog
{
    public interface IFeatureService
    {
        Task<List<FeatureDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<FeatureDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<FeatureDto>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default);
        Task<Guid> AddAsync(FeatureDto model, CancellationToken cancellationToken = default);
        Task Edit(FeatureDto model);
        Task EditAsync(Guid id, FeatureDto model, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
