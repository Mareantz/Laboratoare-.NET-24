using Application.Use_Cases.Commands;
using Application.Use_Cases.ComandHandlers;
using Domain.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace BookManagement.Application.UnitTests
{
    public class DeleteBookCommandHandlerTests
    {
        private readonly IBookRepository repository;
        private readonly DeleteBookCommandHandler handler;

        public DeleteBookCommandHandlerTests()
        {
            repository = Substitute.For<IBookRepository>();
            handler = new DeleteBookCommandHandler(repository);
        }

        [Fact]
        public async Task Given_ValidDeleteBookCommand_When_HandleIsCalled_Then_BookShouldBeDeleted()
        {
            // Arrange
            var bookId = new Guid("0550c1dc-df3f-4dc2-9e29-4388582d2888");
            var command = new DeleteBookCommand(bookId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await repository.Received(1).DeleteAsync(bookId);
            result.Should().Be(Unit.Value);
        }

        //[Fact]
        //public async Task Given_InvalidDeleteBookCommand_When_HandleIsCalled_Then_RepositoryShouldNotBeCalled()
        //{
        //    // Arrange
        //    var command = new DeleteBookCommand(Guid.Empty);

        //    // Act
        //    Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    await act.Should().NotThrowAsync();
        //    await repository.DidNotReceive().DeleteAsync(Arg.Any<Guid>());
        //}
    }
}
