using AutoMapper;
using BackEnd_Angular.DTOs;
using BackEnd_Angular.DTOs.GenreDTOs;
using BackEnd_Angular.Entities;
using BackEnd_Angular.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Angular.Controllers
{
    [ApiController]
	[Route("api/genres")]
	public class GenresController: ControllerBase
	{
		private readonly ILogger<GenresController> logger;
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

		public GenresController(ILogger<GenresController> logger, ApplicationDbContext context, IMapper mapper)
        {
			this.logger = logger;
			this.context = context;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<List<GenreDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
		{
			var queryable = context.Genres.AsQueryable();
			await HttpContext.InsertPaginationParametersInHeader(queryable);
			var genres = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
			return mapper.Map<List<GenreDTO>>(genres);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<GenreDTO>> GetById(int id)
		{
			var genre = await context.Genres.FirstOrDefaultAsync(x => x.Id == id);

			if (genre == null)
			{
				return NotFound();
			}

			return mapper.Map<GenreDTO>(genre);
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromBody] GenreCreationDTO genreCreationDTO)
		{
			var genre = mapper.Map<Genre>(genreCreationDTO);
			context.Add(genre);
			await context.SaveChangesAsync();
			return NoContent();
		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult> Put(int id, [FromBody] GenreCreationDTO genreCreationDTO)
		{
			var genre = await context.Genres.FirstOrDefaultAsync(x => x.Id == id);

			if (genre == null)
			{
				return NotFound();
			}

			genre = mapper.Map(genreCreationDTO, genre);

			await context.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			var exist = await context.Genres.AnyAsync(x => x.Id == id);

			if (!exist)
			{
				return NotFound();
			}

			context.Remove(new Genre() { Id = id });
			await context.SaveChangesAsync();
			return NoContent();
		}
    }
}
