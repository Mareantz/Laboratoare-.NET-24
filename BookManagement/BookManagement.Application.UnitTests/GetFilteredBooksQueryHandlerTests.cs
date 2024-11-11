using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandlers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace BookManagementUnitTests
{
    public class GetFilteredBooksQueryHandlerTests
    {
        private readonly IBookRepository repository;
        private readonly IMapper mapper;

        public GetFilteredBooksQueryHandlerTests()
        {
            repository = Substitute.For<IBookRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Handle_ShouldReturnPagedAndFilteredBooks()
        {
            // Arrange
            var books = GenerateBooks();
            repository.GetAllAsync().Returns(books);
            var command = new GetFilteredBooksQuery {  Page = 1, PageSize = 10 };
            var handler = new GetFilteredBooksQueryHandler(repository, mapper);

            var filteredBooks = books.Where(b => b.Title == "Book 1").ToList();
            var bookDTOs = GenerateBookDTOs(filteredBooks);
            mapper.Map<List<BookDto>>(filteredBooks).Returns(bookDTOs);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
        }

        private List<Book> GenerateBooks()
        {
            return new List<Book>
            {
                new() { Id = Guid.NewGuid(), Title = "Book 1", Author = "Author 1" },
                new() { Id = Guid.NewGuid(), Title = "Book 2", Author = "Author 2" }
            };
        }

        private List<BookDto> GenerateBookDTOs(List<Book> books)
        {
            return books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author
            }).ToList();
        }
    }
}
