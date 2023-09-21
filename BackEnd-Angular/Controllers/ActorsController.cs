using AutoMapper;
using BackEnd_Angular.DTOs.ActorDTOs;
using BackEnd_Angular.Entities;
using BackEnd_Angular.Utilities;
using Microsoft.AspNetCore.Mvc;

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
    }
}
