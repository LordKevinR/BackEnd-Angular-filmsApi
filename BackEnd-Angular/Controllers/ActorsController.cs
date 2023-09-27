using AutoMapper;
using BackEnd_Angular.DTOs;
using BackEnd_Angular.DTOs.ActorDTOs;
using BackEnd_Angular.Entities;
using BackEnd_Angular.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Angular.Controllers
{
	[ApiController]
	[Route("api/actors")]
	public class ActorsController: ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;
		private readonly IAzureStorage azureStorage;
		private readonly string content = "actors";

		public ActorsController(ApplicationDbContext context, IMapper mapper, IAzureStorage azureStorage)
        {
			this.context = context;
			this.mapper = mapper;
			this.azureStorage = azureStorage;
		}

		[HttpGet]
		public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
		{
			var queryable = context.Actors.AsQueryable();
			await HttpContext.InsertPaginationParametersInHeader(queryable);
			var actors = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
			return mapper.Map<List<ActorDTO>>(actors);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<ActorDTO>> GetById(int id)
		{
			var actor = await context.Actors.FirstOrDefaultAsync(a => a.Id == id);

			if(actor == null)
			{
				return NotFound();
			}

			return mapper.Map<ActorDTO>(actor);
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromForm] ActorCreationDTO actorCreationDTO)
		{
			var actor = mapper.Map<Actor>(actorCreationDTO);

			if (actorCreationDTO.Photo != null)
			{
				actor.Photo = await azureStorage.SaveFile(content, actorCreationDTO.Photo);
			}

			context.Add(actor);
			await context.SaveChangesAsync();
			return NoContent();
		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult> Put(int id, [FromForm] ActorCreationDTO actorCreationDTO)
		{
			var actor = await context.Actors.FirstOrDefaultAsync(a => a.Id == id);

			if (actor == null)
			{
				return NotFound();
			}

			actor = mapper.Map(actorCreationDTO, actor);

			if (actorCreationDTO.Photo != null)
			{
				actor.Photo = await azureStorage.EditFile(content, actorCreationDTO.Photo, actor.Photo);
			}

			await context.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			var actor = await context.Actors.FirstOrDefaultAsync(a => a.Id == id);

			if (actor == null)
			{
				return NotFound();
			}

			context.Remove(actor);
			await context.SaveChangesAsync();

			await azureStorage.DeleteFile(actor.Photo, content);

			return NoContent();
		}
	}
}
