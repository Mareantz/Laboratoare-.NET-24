﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
	public interface IBookRepository
	{
		Task<IEnumerable<Book>> GetAllAsync();
		Task<Book> GetByIdAsync(Guid id);
		Task<Guid> AddAsync(Book book);
		Task UpdateAsync(Book book);
		Task DeleteAsync(Guid id);
	}
}