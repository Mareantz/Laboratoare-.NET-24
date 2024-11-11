using MediatR;

namespace Application.Use_Cases.Commands
{
    public class UpdateBookCommand : CreateBookCommand, IRequest
    {
        public Guid Id { get; set; }
    }
}
