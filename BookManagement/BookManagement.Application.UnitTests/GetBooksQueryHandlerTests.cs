using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandlers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace BookManagement.Application.UnitTests
{
    public class GetBooksQueryHandlerTests
    {
        private readonly IBookRepository repository;
        private readonly IMapper mapper;
        public GetBooksQueryHandlerTests()
        {
            repository = Substitute.For<IBookRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public void Given_GetBooksQueryHandler_When_HandleIsCalled_Then_AListOfBooksShouldBeReturned()
        {
            // Arrange
            List<Book> books = GenerateBooks();
            repository.GetAllAsync().Returns(books);
            var query = new GetBooksQuery();
            GenerateBooksDto(books);
            // Act
            var handler = new GetBooksQueryHandler(repository, mapper);
            var result = handler.Handle(query, CancellationToken.None);
            // Assert
            // Assert.NotNull(result);
            result.Should().NotBeNull();
            Assert.Equal(2, result.Result.Count);
            Assert.Equal(books[0].Id, result.Result[0].Id);
        }

        private void GenerateBooksDto(List<Book> books)
        {
            mapper.Map<List<BookDto>>(books).Returns(new List<BookDto>
            {
                new BookDto
                {
                    Id = books[0].Id,
                    Title = books[0].Title,
                    Author = books[0].Author,
                    ISBN = books[0].ISBN
                },
                new BookDto
                {
                    Id = books[1].Id,
                    Title = books[1].Title,
                    Author = books[1].Author,
                    ISBN = books[1].ISBN
                }
            });
        }

        private List<Book> GenerateBooks()
        {
            return new List<Book>
            {
                // generate books
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Book 1",
                    Author = "Author 1",
                    ISBN = "1234567891234"
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Book 2",
                    Author = "Author 2",
                    ISBN = "1234567891235"
                }
            };
        }
    }
}