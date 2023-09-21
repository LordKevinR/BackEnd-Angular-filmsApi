using BackEnd_Angular.DTOs;

namespace BackEnd_Angular.Utilities
{
	public static class IQueryableExtensions
	{
		public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
		{
			return queryable
				.Skip((paginationDTO.Page - 1) * paginationDTO.RecordsPerPAge)
				.Take(paginationDTO.RecordsPerPAge);
		}
	}
}
