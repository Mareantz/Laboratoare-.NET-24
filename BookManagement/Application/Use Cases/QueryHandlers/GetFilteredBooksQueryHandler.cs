using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Utils;
using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using Gridify;
using MediatR;

namespace Application.Use_Cases.QueryHandlers
{
    public class GetFilteredBooksQueryHandler : IRequestHandler<GetFilteredBooksQuery, Result<PagedResult<BookDto>>>
    {
        private readonly IBookRepository repository;
        private readonly IMapper mapper;

        public GetFilteredBooksQueryHandler(IBookRepository repository, IMapper mapper)
        {
            this.repository=repository;
            this.mapper=mapper;
        }
        public async Task<Result<PagedResult<BookDto>>> Handle(GetFilteredBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await repository.GetAllAsync();
            var query = books.AsQueryable();

            // Apply paging
            var pagedBooks = query.ApplyPaging(request.Page, request.PageSize);

            var bookDtos = mapper.Map<List<BookDto>>(pagedBooks);

            var pagedResult = new PagedResult<BookDto>(bookDtos, query.Count());

            return Result<PagedResult<BookDto>>.Success(pagedResult);
        }
    }
}
