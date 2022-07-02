using DevTubeCommerce.Application.Contract.Dto.Catalog;
using DevTubeCommerce.Application.Contract.Interfaces.Catalog;
using DevTubeCommerce.Domain.Core.Catalogs.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevTubeCommerce.Infrastructure.Patterns;
using DevTubeCommerce.Domain.Core.Base;
using DevTubeCommerce.Framework.Exceptions;
using DevTubeCommerce.Framework.Global;

namespace DevTubeCommerce.Application.Services.Catalog
{
    public class FeatureService : IFeatureService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFeatureRepository featureRepository;

        public FeatureService(IUnitOfWork unitOfWork, IFeatureRepository featureRepository)
        {
            this.unitOfWork = unitOfWork;
            this.featureRepository = featureRepository;
        }
        public async Task<Guid> AddAsync(FeatureDto model, CancellationToken cancellationToken = default)
        {
            var feature = Feature.CreateNew(model.Title, model.Description, model.SortOrder);
            await featureRepository.Insert(feature);
            await unitOfWork.SaveChangesAsync();

            return feature.Id.Value;
            //insert to db
            //=> 1.call Repository
            //=> 2.create DbContext
        }

        public async Task Edit(FeatureDto model)
        {
            var feature = Feature.CreateNewForUpdate(model.Id, model.Title, model.Description, model.SortOrder);
            await featureRepository.Update(feature);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task EditAsync(Guid id, FeatureDto model, CancellationToken cancellationToken = default)
        {
            var featureId = new FeatureId(id);
            var feature = await featureRepository.GetByIdAsync(featureId, cancellationToken);
            if (feature == null)
                throw new NotFoundException(Error.FeatureNotFound);

            feature.Update(model.Title, model.Description, model.SortOrder);
            await featureRepository.Update(feature);
            await unitOfWork.SaveChangesAsync();

        }

        public async Task<List<FeatureDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return (await featureRepository.GetFeaturesAsync(cancellationToken)).Select(f => new FeatureDto()
            {
                Id = f.Id.Value,
                Description = f.Description,
                SortOrder = f.SortOrder,
                Title = f.Title
            }).ToList();
        }

        public async Task<FeatureDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var feature = await featureRepository.GetByIdAsync(new FeatureId(id),cancellationToken);
            if (feature == null)
                throw new NotFoundException(Error.FeatureNotFound);
            return new FeatureDto
            {
                Title = feature.Title,
                Description = feature.Description,
                Id = id,
                SortOrder = feature.SortOrder,
            };
        }

        public async Task<List<FeatureDto>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
        {
            var featureIds=ids.Select(x=>new FeatureId(x)).ToList();
            var features= await featureRepository.GetByIdsAsync(featureIds, cancellationToken);
            return features.Select(x => new FeatureDto() 
            {
            Id=x.Id.Value,
            Description=x.Description,
            SortOrder=x.SortOrder,
            Title=x.Title
            }).ToList();
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            featureRepository.Delete(new FeatureId(id));
            await unitOfWork.SaveChangesAsync();
        }
    }
}
