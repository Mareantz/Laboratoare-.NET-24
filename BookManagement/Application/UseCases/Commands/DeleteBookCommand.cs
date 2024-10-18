using MediatR;

namespace Application.UseCases.Commands
{
	public class DeleteBookCommand : IRequest
	{
		public Guid Id { get; set; }
	}
}
