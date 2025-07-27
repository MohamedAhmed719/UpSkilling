using Microsoft.AspNetCore.Mvc;
using Task.Api.Contracts.Books;
using Task.Api.Services;

namespace Task.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(IBookService bookService) : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _bookService.GetAsync(id, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        private readonly IBookService _bookService = bookService;
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] BookRequest request,CancellationToken cancellationToken)
        {
            var result = await _bookService.AddAsync(request, cancellationToken);

            return result.IsSuccess ? CreatedAtAction(nameof(Get),new { result.Value!.Id},result.Value) : BadRequest(result.Error);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var books = await _bookService.GetAllAsync(cancellationToken);

            return Ok(books);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BookRequest request,CancellationToken cancellationToken)
        {
            var result = await _bookService.UpdateAsync(id, request, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _bookService.DeleteAsync(id, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
    }
}
