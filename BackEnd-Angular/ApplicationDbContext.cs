using BackEnd_Angular.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Angular
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Genre> Genres { get; set; }
		public DbSet<Actor> Actors { get; set; }
	}
}
