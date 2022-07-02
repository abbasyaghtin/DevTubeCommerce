using AutoMapper;
using DevTubeCommerce.API.Forms.Catalogs.Products;
using DevTubeCommerce.Application.Contract.Commands.Products;
using DevTubeCommerce.Application.Contract.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevTubeCommerce.API.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        public ProductController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(GetProductQueryResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(/*typeof(ErrorModel),*/ StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] AddProductForm addProductForm, CancellationToken cancellationToken = default)
        {
            AddProductCommand command = mapper.Map<AddProductCommand>(addProductForm);
            var addProductCommandResponse = await mediator.Send(command, cancellationToken);
            var query = new GetProductByIdQuery { Id = addProductCommandResponse.productId };
            var result = await mediator.Send(query, cancellationToken);
            return Created(String.Empty, result);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetProductQueryResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var query = new GetProductByIdQuery { Id = id };
            var result = await mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetProductQueryResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductsAsync(CancellationToken cancellationToken = default)
        {
            var query = new GetAllProductsQuery();
            var result = await mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}/UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(/*typeof(ErrorModel),*/ StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductForm updateProductForm, CancellationToken cancellationToken = default) 
        {
            var command = mapper.Map<UpdateProductCommand>(updateProductForm);
            command.Id = id;
            await mediator.Send(command, cancellationToken);
            return NoContent();
        }

    }
}
