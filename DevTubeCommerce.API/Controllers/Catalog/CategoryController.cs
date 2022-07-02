using AutoMapper;
using DevTubeCommerce.API.Forms.Catalogs.Categories;
using DevTubeCommerce.API.OutputModels.Catalogs.Categories;
using DevTubeCommerce.API.OutputModels.Catalogs.Features;
using DevTubeCommerce.Application.Contract.Dto.Catalog;
using DevTubeCommerce.Application.Contract.Interfaces.Catalog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevTubeCommerce.API.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IFeatureService featureService;
        private readonly IMapper mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper, IFeatureService featureService)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
            this.featureService = featureService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CategoryModel), StatusCodes.Status201Created)]
        [ProducesResponseType(/*typeof(ErrorModel),*/ StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AddCategoryForm addCategoryForm, CancellationToken cancellationToken = default)
        {
            var CategoryDto = mapper.Map<CategoryDto>(addCategoryForm);
            var categoryId = await categoryService.AddCategoryAsync(CategoryDto, cancellationToken);
            var category = await categoryService.GetByIdAsync(categoryId, cancellationToken);
            var result = mapper.Map<CategoryModel>(category);
            await GetFeatures(category, result, cancellationToken);
            return Created(String.Empty, result);
        }


        [HttpGet]
        [ProducesResponseType(typeof(CategoryModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken = default)
        {
            var categories = await categoryService.GetCategories(cancellationToken);
            var result = mapper.Map<List<CategoryModel>>(categories);
            await GetFeatures(categories, result, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var category = await categoryService.GetByIdAsync(id, cancellationToken);
            var result = mapper.Map<CategoryModel>(category);
            await GetFeatures(category, result, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}/UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(/*typeof(ErrorModel),*/ StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateFeature([FromRoute] Guid id, [FromBody] UpdateCategoryForm updateFeatureForm, CancellationToken cancellationToken = default)
        {
            var categoryDto = mapper.Map<CategoryDto>(updateFeatureForm);
            await categoryService.UpdateCategoryAsync(id, categoryDto, cancellationToken);
            return NoContent();
        }

        [NonAction] //Just For Read Better
        private async Task GetFeatures(CategoryDto category, CategoryModel categoryModel, CancellationToken cancellationToken)
        {
            categoryModel.Features = (await featureService.GetByIdsAsync(category.FeatureIds, cancellationToken))
                                .Select(x => new FeatureModel()
                                {
                                    Id = x.Id,
                                    Description = x.Description,
                                    SortOrder = x.SortOrder,
                                    Title = x.Title
                                }).ToList();
        }

        [NonAction] //overload
        private async Task GetFeatures(List<CategoryDto> categories, List<CategoryModel> categoryModels, CancellationToken cancellationToken)
        {
            foreach (var categoryModel in categoryModels)
            {
                var featureIds = categories.Where(x => x.Id == categoryModel.Id).SelectMany(x => x.FeatureIds).ToList();
                categoryModel.Features = (await featureService.GetByIdsAsync(featureIds, cancellationToken))
                                    .Select(x => new FeatureModel()
                                    {
                                        Id = x.Id,
                                        Description = x.Description,
                                        SortOrder = x.SortOrder,
                                        Title = x.Title
                                    }).ToList();
            }
        }
    }
}
