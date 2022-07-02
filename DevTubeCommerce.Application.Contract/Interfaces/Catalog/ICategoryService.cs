using DevTubeCommerce.Application.Contract.Dto.Catalog;
using System.Collections.Generic;

namespace DevTubeCommerce.Application.Contract.Interfaces.Catalog
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetCategories(CancellationToken cancellationToken=default);
        Task<Guid> AddCategoryAsync(CategoryDto categoryDto,CancellationToken cancellationToken = default);
        Task<CategoryDto> GetByIdAsync(Guid categoryId,CancellationToken cancellationToken=default);
        Task UpdateCategoryAsync(Guid id, CategoryDto categoryDto, CancellationToken cancellationToken);
    }
}
