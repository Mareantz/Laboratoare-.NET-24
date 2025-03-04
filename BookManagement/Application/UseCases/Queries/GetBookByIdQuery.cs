﻿using Application.DTOs;
using MediatR;

namespace Application.UseCases.Queries
{
	public class GetBookByIdQuery : IRequest<BookDTO>
	{
		public Guid Id { get; set; }
	}
}
