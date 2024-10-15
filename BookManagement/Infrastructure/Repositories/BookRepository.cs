using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public Task DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Book>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<Book> GetByIdAsync(Guid id)
		{
			return await context.Books.FindAsync(id);
		}

		public Task UpdateAsync(Book book)
		{
			throw new NotImplementedException();
		}
	}
}
