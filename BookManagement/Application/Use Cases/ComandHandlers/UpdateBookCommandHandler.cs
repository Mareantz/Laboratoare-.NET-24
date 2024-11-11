using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.ComandHandlers
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IBookRepository repository;
        private readonly IMapper mapper;

        public UpdateBookCommandHandler(IBookRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = mapper.Map<Book>(request);
            return repository.UpdateAsync(book);
        }
    }
}
