using MediatR;

namespace Application.Use_Cases.Commands
{
    public record DeleteBookCommand(Guid Id) : IRequest<Unit>;
}
