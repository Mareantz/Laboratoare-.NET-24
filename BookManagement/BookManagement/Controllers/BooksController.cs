using Application.DTOs;
using Application.UseCases.Commands;
using Application.UseCases.Queries;
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
		public async Task<ActionResult<Guid>> CreateBook(CreateBookCommand command)
		{
			return await mediator.Send(command);
		}

		[HttpGet]
		[Route("/{id}")]
		public async Task<ActionResult<BookDTO>> GetBookById([FromRoute] Guid id)
		{
			var query = new GetBookByIdQuery { Id = id };
			return await mediator.Send(query);
		}

		[HttpGet]
		public async Task<ActionResult<List<BookDTO>>> GetAllBooks([FromQuery] GetAllBooksQuery query)
		{
			return await mediator.Send(query);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateBook(UpdateBookCommand command)
		{
			await mediator.Send(command);
			return Ok();
		}
		[HttpDelete]
		public async Task<ActionResult> DeleteBook(DeleteBookCommand command)
		{
			await mediator.Send(command);
			return Ok();
		}

	}
}
