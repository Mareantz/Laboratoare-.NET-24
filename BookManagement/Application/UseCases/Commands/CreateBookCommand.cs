using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Commands
{
	public class CreateBookCommand : IRequest<Guid>
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public string ISBN { get; set; }
		public DateTime PublicationDate { get; set; }
	}
}
