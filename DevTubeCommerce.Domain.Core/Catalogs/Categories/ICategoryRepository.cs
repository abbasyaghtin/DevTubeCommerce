using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Domain.Core.Catalogs.Categories
{
    public interface ICategoryRepository
    {
        Task<CategoryId> InsertCategoryAsync(Category category, CancellationToken cancellationToken = default);
        Task<Category> GetByIdAsync(CategoryId categoryId, CancellationToken cancellationToken);
        Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken);
    }
}
