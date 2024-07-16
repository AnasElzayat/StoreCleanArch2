using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Features.Products.Commands.CreateProduct;
using Store.Application.Features.Products.Queries.GetProductsList;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetProductsListQuery();
            var products = await _mediator.Send(query);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var query = new GetProductsListQuery(p => p.Id == id);
            var products = await _mediator.Send(query);
            if (products == null)
            {
                return NotFound();
            }
            var product = products.FirstOrDefault();

            

            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productViewModel = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { id = productViewModel.Id }, productViewModel);
        }
    }
}
