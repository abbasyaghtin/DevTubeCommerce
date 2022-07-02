using DevTubeCommerce.Application.Contract.Dto.Catalog;
using DevTubeCommerce.Application.Contract.Interfaces.Catalog;
using DevTubeCommerce.Domain.Core.Catalogs.Categories;
using DevTubeCommerce.Domain.Core.Catalogs.Features;
using DevTubeCommerce.Framework.Exceptions;
using DevTubeCommerce.Framework.Global;
using DevTubeCommerce.Infrastructure.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Application.Services.Catalog
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork unitOfWork;
        public CategoryService(ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork)
        {
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Guid> AddCategoryAsync(CategoryDto categoryDto, CancellationToken cancellationToken = default)
        {
            List<FeatureId> featureIds = new();
            categoryDto.FeatureIds.ForEach(x =>
            {
                var result = new FeatureId(x);
                featureIds.Add(result);
            });
            var category = Category.CreateNew(categoryDto.CategoryName, categoryDto.IsActive, categoryDto.Description, featureIds);
            await categoryRepository.InsertCategoryAsync(category, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            return category.Id.Value;
        }

        public async Task<CategoryDto> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            var category = await categoryRepository.GetByIdAsync(new CategoryId(categoryId), cancellationToken);
            if (category == null)
                throw new NotFoundException(Error.CategoryNotFound);
            var featureIds = category.CategoryFeatures.Select(x => x.FeatureId).ToList();
            return new CategoryDto
            {
                Id = category.Id.Value,
                CategoryName = category.CategoryName,
                Description = category.Description,
                IsActive = category.IsActive,
                FeatureIds = category.CategoryFeatures.Select(x => x.FeatureId.Value).ToList()
            };
        }

        public async Task<List<CategoryDto>> GetCategories(CancellationToken cancellationToken = default)
        {
            var categories = await categoryRepository.GetCategoriesAsync(cancellationToken);
            if (categories?.Count <= 0)
                throw new NotFoundException(Error.CategoryNotFound);

            return categories.Select(x => new CategoryDto()
            {
                Id = x.Id.Value,
                CategoryName = x.CategoryName,
                Description = x.Description,
                IsActive = x.IsActive,
                FeatureIds = x.CategoryFeatures.Select(x => x.FeatureId.Value).ToList()
            }).ToList();
        }

        public async Task UpdateCategoryAsync(Guid id, CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            var categoryId = new CategoryId(id);
            var category = await categoryRepository.GetByIdAsync(categoryId, cancellationToken);
            if (category == null)
                throw new NotFoundException(Error.CategoryNotFound);

            var oldfeatureIds = category.CategoryFeatures.Select(x => x.FeatureId).ToList();
            var currentFeatureIds = categoryDto.FeatureIds.Select(x => new FeatureId(x)).ToList();

            category.Update(categoryId, categoryDto.CategoryName, categoryDto.IsActive,
                           categoryDto.Description, oldfeatureIds, currentFeatureIds);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
