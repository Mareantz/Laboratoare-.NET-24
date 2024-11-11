using Domain.Common;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateBookCommand : IRequest<Result<Guid>>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
