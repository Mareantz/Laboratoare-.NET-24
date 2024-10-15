﻿using Application.DTOs;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries
{
	public class GetBookByIdQuery : IRequest<BookDTO>
	{
		public Guid Id { get; set; }
	}
}
