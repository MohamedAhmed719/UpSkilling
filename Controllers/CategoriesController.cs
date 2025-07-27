using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Api.Services;

namespace Task.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpGet("{id}")]

        public async Task<IActionResult> Get([FromRoute] int id,CancellationToken cancellationToken) 
        {

            var result = await _categoryService.GetAsync(id, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetAllAsync(cancellationToken);

            return Ok(categories);
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] CategoryRequest request,CancellationToken cancellationToken)
        {
            var result = await _categoryService.AddAsync(request,cancellationToken);

            return result.IsSuccess ? CreatedAtAction(nameof(Get),new {result.Value!.Id},result.Value): BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoryRequest request,CancellationToken cancellationToken)
        {
            var result = await _categoryService.UpdateAsync(id, request, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id,CancellationToken cancellationToken)
        {
            var result = await _categoryService.DeleteAsync(id, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
    }
}
