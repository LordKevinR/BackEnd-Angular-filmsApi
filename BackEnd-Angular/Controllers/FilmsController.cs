using AutoMapper;
using BackEnd_Angular.DTOs.ActorDTOs;
using BackEnd_Angular.DTOs.FilmDTOs;
using BackEnd_Angular.DTOs.GenreDTOs;
using BackEnd_Angular.DTOs.TheaterDTOs;
using BackEnd_Angular.Entities;
using BackEnd_Angular.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Angular.Controllers
{
	[ApiController]
	[Route("api/films")]
	public class FilmsController: ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;
		private readonly IAzureStorage azureStorage;
		private readonly string _container = "films";

		public FilmsController(ApplicationDbContext context, IMapper mapper, IAzureStorage azureStorage)
        {
			this.context = context;
			this.mapper = mapper;
			this.azureStorage = azureStorage;
		}

		[HttpGet]
		public async Task<ActionResult<LandingPageDTO>> Get()
		{
			var top = 6;
			var today = DateTime.Today;

			var nextRealeases = await context.Films
				.Where(x => x.ReleaseDate > today)
				.OrderBy(x => x.ReleaseDate)
				.Take(top)
				.ToListAsync();

			var inTheaters = await context.Films
				.Where(x => x.InTheaters)
				.OrderBy(x => x.ReleaseDate)
				.Take(top)
				.ToListAsync();

			var result = new LandingPageDTO();
			result.NextReleases = mapper.Map<List<FilmDTO>>(nextRealeases);
			result.InTheaters = mapper.Map<List<FilmDTO>>(inTheaters);

			return result;
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<FilmDTO>> GetById(int id)
		{
			var film = await context.Films
				.Include(x => x.GenresFilms).ThenInclude(x => x.Genre)
				.Include(x => x.ActorsFilms).ThenInclude(x => x.Actor)
				.Include(x => x.TheatersFilms).ThenInclude(x => x.Theater)
				.FirstOrDefaultAsync(x => x.Id == id);

			if (film is null) { return NotFound(); }

			var dto = mapper.Map<FilmDTO>(film);
			dto.Actors = dto.Actors.OrderBy( x => x.Order).ToList();

			return dto;
		}

		[HttpGet("PutGet/{id:int}")]
		public async Task<ActionResult<FilmsPutGetDTO>> PutGet(int id)
		{
			var filmActionResult = await GetById(id);
			if(filmActionResult.Result is NotFoundResult) { return NotFound(); }

			var film = filmActionResult.Value;

			var selectedGenresId = film.Genres.Select(x => x.Id).ToList();
			var noSelectedGenres = await context.Genres
				.Where(x => !selectedGenresId.Contains(x.Id))
				.ToListAsync();

			var selectedTheatersId = film.Theaters.Select(x => x.Id).ToList();
			var noSelectedTheaters = await context.Theaters
				.Where(x => !selectedTheatersId.Contains(x.Id))
				.ToListAsync();

			var noSelectedGenresDTO = mapper.Map<List<GenreDTO>>(noSelectedGenres);
			var noSelectedTheatersDTO = mapper.Map<List<TheaterDTO>>(noSelectedTheaters);

			var response = new FilmsPutGetDTO();
			response.Film = film;
			response.SelectedGenres = film.Genres;
			response.NoSelectedGenres = noSelectedGenresDTO;
			response.SelectedTheaters = film.Theaters;
			response.NoSelectedTheaters = noSelectedTheatersDTO;
			response.Actors = film.Actors;
			return response;
		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult> Put(int id, [FromBody] FilmCreationDTO filmCreationDTO)
		{
			var film = await context.Films
				.Include(x => x.ActorsFilms)
				.Include(x => x.GenresFilms)
				.Include(x => x.TheatersFilms)
				.FirstOrDefaultAsync(x => x.Id == id);

			if (film is null)
			{
				return NotFound();
			}

			film = mapper.Map(filmCreationDTO, film);

			if (filmCreationDTO.Poster != null)
			{
				film.Poster = await azureStorage.EditFile(_container, filmCreationDTO.Poster, film.Poster);
			}

			WriteActorsOrder(film);

			await context.SaveChangesAsync();
			return NoContent();
		}


		[HttpPost]
		public async Task<ActionResult> Post([FromForm] FilmCreationDTO filmCreationDTO)
		{
			var film = mapper.Map<Film>(filmCreationDTO);

			if (filmCreationDTO.Poster != null)
			{
				film.Poster = await azureStorage.SaveFile(_container, filmCreationDTO.Poster);
			}

			WriteActorsOrder(film);

			context.Add(film);
			await context.SaveChangesAsync();
			return NoContent();
		}

		[HttpGet("PostGet")]
		public async Task<ActionResult<FilmsPostGetDTO>> PostGet()
		{
			var theaters = await context.Theaters.ToListAsync();
			var genres = await context.Genres.ToListAsync();

			var theatersDTO = mapper.Map<List<TheaterDTO>>(theaters);
			var genresDTO = mapper.Map<List<GenreDTO>>(genres);

			return new FilmsPostGetDTO() { Theaters = theatersDTO, Genres = genresDTO };
		}

		private void WriteActorsOrder(Film film)
		{
			if (film.ActorsFilms != null)
			{
				for (int i = 0; i < film.ActorsFilms.Count; i++)
				{
					film.ActorsFilms[i].Order = i;
				}
			}
		}
    }
}
