using Application.UseCases.Commands;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.CommandHandlers
{
	public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
	{
		private readonly IBookRepository repository;

		public CreateBookCommandHandler(IBookRepository repository)
		{
			this.repository = repository;
		}
		public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
		{
			var book = new Book
			{
				Title = request.Title,
				Author = request.Author,
				ISBN = request.ISBN,
				PublicationDate = request.PublicationDate,
			};
			return await repository.AddAsync(book);
		}
	}
}
