using Application.DTOs;
using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using Application.Utils;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator mediator;

        public BooksController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Result<Guid>>> CreateBook(CreateBookCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetBookById), new { Id = result.Data }, result.Data);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookDto>> GetBookById(Guid id)
        {
            return await mediator.Send(new GetBookByIdQuery { Id = id });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateBookCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("The id should be identical with command.Id");
            }

            await mediator.Send(command);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await mediator.Send(new DeleteBookCommand(id));
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
        {
            var result = await mediator.Send(new GetBooksQuery());
            return Ok(result);
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<PagedResult<BookDto>>> GetFilteredBooks([FromQuery] int page, [FromQuery] int pageSize)
        {
            var query = new GetFilteredBooksQuery
            {
                Page = page,
                PageSize = pageSize
            };
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
