using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext context;

        public BookRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Result<Guid>> AddAsync(Book book)
        {
            try
            {
                await context.Books.AddAsync(book);
                await context.SaveChangesAsync();
                return Result<Guid>.Success(book.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.InnerException!.ToString());
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var book = await context.Books.FindAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with Id {id} not found.");
            }

            context.Books.Remove(book);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await context.Books.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(Guid id)
        {
            return await context.Books.FindAsync(id);
        }

        public Task UpdateAsync(Book book)
        {
            context.Entry(book).State = EntityState.Modified;
            return context.SaveChangesAsync();
        }
    }
}
