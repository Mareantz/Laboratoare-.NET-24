using Application.DTOs;
using Application.Utils;
using Domain.Common;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetFilteredBooksQuery : IRequest<Result<PagedResult<BookDto>>>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
