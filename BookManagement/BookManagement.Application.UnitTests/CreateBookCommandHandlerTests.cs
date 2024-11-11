using Application.Use_Cases.Commands;
using Application.Use_Cases.ComandHandlers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace BookManagement.Application.UnitTests
{
    public class CreateBookCommandHandlerTests
    {
        private readonly IBookRepository repository;
        private readonly IMapper mapper;
        private readonly CreateBookCommandHandler handler;

        public CreateBookCommandHandlerTests()
        {
            repository = Substitute.For<IBookRepository>();
            mapper = Substitute.For<IMapper>();
            handler = new CreateBookCommandHandler(repository, mapper);
        }

        [Fact]
        public async Task Given_ValidCreateBookCommand_When_HandleIsCalled_Then_BookShouldBeCreated()
        {
            // Arrange
            var command = new CreateBookCommand
            {
                Title = "New Book",
                Author = "Author",
                ISBN = "1234567891234",
                PublicationDate = DateTime.UtcNow
            };
            var book = new Book
            {
                Id = new Guid("0550c1dc-df3f-4dc2-9e29-4388582d2889"),
                Title = command.Title,
                Author = command.Author,
                ISBN = command.ISBN,
                PublicationDate = command.PublicationDate
            };
            mapper.Map<Book>(command).Returns(book);
            repository.AddAsync(book).Returns(Result<Guid>.Success(book.Id));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await repository.Received(1).AddAsync(book);
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(book.Id);
        }

        [Fact]
        public async Task Given_InvalidCreateBookCommand_When_HandleIsCalled_Then_FailureResultShouldBeReturned()
        {
            // Arrange
            var command = new CreateBookCommand
            {
                Title = "New Book",
                Author = "Author",
                ISBN = "1234567891234",
                PublicationDate = DateTime.UtcNow
            };
            var book = new Book
            {
                Id = new Guid("0550c1dc-df3f-4dc2-9e29-4388582d2899"),
                Title = command.Title,
                Author = command.Author,
                ISBN = command.ISBN,
                PublicationDate = command.PublicationDate
            };
            mapper.Map<Book>(command).Returns(book);
            repository.AddAsync(book).Returns(Result<Guid>.Failure("Error adding book"));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await repository.Received(1).AddAsync(book);
            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Error adding book");
        }
    }
}
