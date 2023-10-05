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
