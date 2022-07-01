using AutoMapper;
using DevTubeCommerce.API.Forms.Catalogs.Features;
using DevTubeCommerce.API.OutputModels.Catalogs.Features;
using DevTubeCommerce.Application.Contract.Dto.Catalog;
using DevTubeCommerce.Application.Contract.Interfaces.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace DevTubeCommerce.API.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService featureService;
        private readonly IMapper mapper;
        public FeatureController(IFeatureService featureService, IMapper mapper)
        {
            this.featureService = featureService;
            this.mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(FeatureModel), StatusCodes.Status201Created)]
        [ProducesResponseType(/*typeof(ErrorModel),*/ StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AddFeatureForm addFeatureForm, CancellationToken cancellationToken = default)
        {
            var featureDto = mapper.Map<FeatureDto>(addFeatureForm);
            var featurId = await featureService.AddAsync(featureDto, cancellationToken);
            var feature = await featureService.GetByIdAsync(featurId);
            var result = mapper.Map<FeatureModel>(feature);
            return Created(String.Empty, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(FeatureModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFeatures(CancellationToken cancellationToken = default)
        {
            var features = await featureService.GetAllAsync(cancellationToken);
            var result = mapper.Map<List<FeatureModel>>(features);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FeatureModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var feature = await featureService.GetByIdAsync(id);
            var result = mapper.Map<FeatureModel>(feature);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}/UpdateFeature")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(/*typeof(ErrorModel),*/ StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateFeature([FromRoute] Guid id, [FromBody] UpdateFeatureForm updateFeatureForm, CancellationToken cancellationToken = default)
        {
            var featureDto = mapper.Map<FeatureDto>(updateFeatureForm);
            await featureService.EditAsync(id, featureDto, cancellationToken);
            return NoContent();
        }


        //********Hard Delete Is Wrong But Just For Example!***********
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFeature([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            await featureService.RemoveAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
