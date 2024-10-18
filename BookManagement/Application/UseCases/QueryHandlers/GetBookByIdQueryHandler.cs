using Application.DTOs;
using Application.UseCases.Queries;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.QueryHandlers
{
	public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDTO>
	{
		private readonly IBookRepository repository;

		public GetBookByIdQueryHandler(IBookRepository repository) 
		{
			this.repository = repository;
		}

		public async Task<BookDTO> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
		{
			var book = await repository.GetByIdAsync(request.Id);

			return new BookDTO
			{
				Id = book.Id,
				Title = book.Title,
				Author = book.Author,
				PublicationDate = book.PublicationDate
			};
		}
	}
}
