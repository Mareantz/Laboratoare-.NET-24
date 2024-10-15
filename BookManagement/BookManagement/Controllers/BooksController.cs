using Application.DTOs;
using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
		public async Task<ActionResult<BookDTO>> GetBookById([FromQuery] GetBookByIdQuery query)
		{
			return await mediator.Send(query);
		}
	}
}
