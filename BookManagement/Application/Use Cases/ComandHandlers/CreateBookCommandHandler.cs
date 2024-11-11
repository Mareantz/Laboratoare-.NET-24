using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.ComandHandlers
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<Guid>>
    {
        private readonly IBookRepository repository;
        private readonly IMapper mapper;

        public CreateBookCommandHandler(IBookRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<Guid>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = mapper.Map<Book>(request);
           
            var result = await repository.AddAsync(book);
            if (result.IsSuccess)
            {
                return Result<Guid>.Success(result.Data);
            }
            return Result<Guid>.Failure(result.ErrorMessage);
        }
    }
}
