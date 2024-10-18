using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistance;
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
        public async Task<Guid> AddAsync(Book book)
		{
			await context.Books.AddAsync(book);
			await context.SaveChangesAsync();
			return book.Id;
		}

		public async Task DeleteAsync(Guid id)
		{
			var book = await context.Books.FindAsync(id);
			context.Remove(book);
			await context.SaveChangesAsync();
		}

		public async Task<List<Book>> GetAllAsync()
		{
			return await context.Books.ToListAsync();
		}

		public async Task<Book> GetByIdAsync(Guid id)
		{
			return await context.Books.FindAsync(id);
		}

		public async Task UpdateAsync(Book book)
		{
			await context.SaveChangesAsync();
		}
	}
}
