using Microsoft.EntityFrameworkCore;

namespace BackEnd_Angular.Utilities
{
	public static class HttpContextExtensions
	{
		public async static Task InsertPaginationParametersInHeader<T>(this HttpContext httpContext, IQueryable<T> queryable)
		{
			if (httpContext == null) { throw new ArgumentException(nameof(httpContext)); }

			double amount = await queryable.CountAsync();
			httpContext.Response.Headers.Add("totalRecordsNumber", amount.ToString());
		}
	}
}
