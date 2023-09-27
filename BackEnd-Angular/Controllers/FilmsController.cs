using AutoMapper;
using BackEnd_Angular.DTOs.ActorDTOs;
using BackEnd_Angular.DTOs.FilmDTOs;
using BackEnd_Angular.Entities;
using BackEnd_Angular.Utilities;
using Microsoft.AspNetCore.Mvc;

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
