using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{ 
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddMediatR(
				cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			//services.AddAutoMapper(typeof(MappingProfile));

			return services;
		}
	}
}
