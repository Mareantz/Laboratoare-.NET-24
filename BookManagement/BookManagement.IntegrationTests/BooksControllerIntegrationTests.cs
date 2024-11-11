using Application.Use_Cases.Commands;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace BookManagement.IntegrationTests
{
    public class BooksControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        private readonly WebApplicationFactory<Program> factory;
        private readonly ApplicationDbContext dbContext;

        private string BaseUrl = "/api/v1/books";
        public BooksControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            this.factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                            typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(descriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });
                });
            });

            var scope = this.factory.Services.CreateScope();
            dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
        }

        [Fact]
        public void GivenBooks_WhenGetAllIsCalled_ThenReturnsTheRightContentType()
        {
            // arrange
            var client = factory.CreateClient();

            // act
            var response = client.GetAsync(BaseUrl);

            // assert
            response.Result.EnsureSuccessStatusCode();
            response.Result.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
        }

        [Fact]
        public void GivenExistingBooks_WhenGetAllIsCalled_ThenReturnsTheRightBooks()
        {
            // arrange
            var client = factory.CreateClient();
            CreateSUT();

            // act
            var response = client.GetAsync(BaseUrl);

            // assert
            response.Result.EnsureSuccessStatusCode();
            var books = response.Result.Content.ReadAsStringAsync().Result;
            books.Should().Contain("Book 1");
        }

        [Fact]
        public async void GivenValidBook_WhenCreatedIsCalled_Then_ShouldAddToDatabaseTheBook()
        {
            // Arrange
            var client = factory.CreateClient();

            var command = new CreateBookCommand
            {
                Title = "Title 1",
                Author = "Author 1",
                ISBN = "ISBN 1",
                PublicationDate = new DateTime(2021, 1, 1)
            };

            // Act
            await client.PostAsJsonAsync(BaseUrl, command);

            // Assert
            var book = dbContext.Books.FirstOrDefaultAsync(b => b.Title == "Title 1");
            book.Should().NotBeNull();
        }
        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }


        private void CreateSUT()
        {
            var book = new Book
            {
                Title = "Book 1",
                Author = "Author 1",
                ISBN = "ISBN 1",
                PublicationDate = new DateTime(2021, 1, 1)
            };
            dbContext.Books.Add(book);
            dbContext.SaveChanges();
        }
    }
}