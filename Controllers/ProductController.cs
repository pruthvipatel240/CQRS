using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CQRS_Demo.ProductFeatures.Commands;
using CQRS_Demo.ProductFeatures.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;


namespace CQRS_Demo.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private readonly IMediator _mediator;
      public ProductController(IMediator mediator)
    {
      _mediator = mediator;
    }
    //protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
      return Ok(await _mediator.Send(command));
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      return Ok(await _mediator.Send(new GetAllProductsQuery()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    
    {
      return Ok(await _mediator.Send(new GetProductByIdQuery { Id = id }));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {

      return Ok(await _mediator.Send(new DeleteProductByIdCommand { Id = id }));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProductCommand command)
    {
      if (id != command.Id)
      {
        return BadRequest();
      }
      return Ok(await _mediator.Send(command));
    }
  }
}
