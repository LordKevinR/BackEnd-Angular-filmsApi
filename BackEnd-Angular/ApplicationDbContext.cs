using BackEnd_Angular.Entities;
using BackEnd_Angular.Entities.RelationshipEntities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Angular
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ActorsFilms>()
				.HasKey(x => new { x.ActorId, x.FilmId });

			modelBuilder.Entity<TheatersFilms>()
				.HasKey(x => new { x.TheaterId, x.FilmId });

			modelBuilder.Entity<GenresFilms>()
				.HasKey(x => new { x.GenreId, x.FilmId });


			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Genre> Genres { get; set; }
		public DbSet<Actor> Actors { get; set; }
		public DbSet<Theater> Theaters { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<ActorsFilms> ActorsFilms { get; set; }
        public DbSet<GenresFilms> GenresFilms { get; set; }
        public DbSet<TheatersFilms> TheatersFilms { get; set; }

    }
}
