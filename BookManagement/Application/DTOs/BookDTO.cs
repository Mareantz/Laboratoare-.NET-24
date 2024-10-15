using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
	public class BookDTO
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string ISBN { get; set; }
		public DateTime PublicationDate { get; set; }
	}
}
