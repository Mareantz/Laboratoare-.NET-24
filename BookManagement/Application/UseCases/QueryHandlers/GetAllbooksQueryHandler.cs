using Application.DTOs;
using Application.UseCases.Queries;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.QueryHandlers
{
	public class GetAllbooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<BookDTO>>
	{
		private readonly IBookRepository repository;

		public GetAllbooksQueryHandler(IBookRepository repository)
		{
			this.repository = repository;
		}
		public async Task<List<BookDTO>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
		{
			var books = await repository.GetAllAsync();

			return books.Select(book => new BookDTO
			{
				Id = book.Id,
				Title = book.Title,
				Author = book.Author,
				ISBN = book.ISBN,
				PublicationDate = book.PublicationDate
			}).ToList();
		}
	}
}
