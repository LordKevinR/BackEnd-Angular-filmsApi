using AutoMapper;
using BackEnd_Angular.DTOs;
using BackEnd_Angular.DTOs.GenreDTOs;
using BackEnd_Angular.DTOs.TheaterDTOs;
using BackEnd_Angular.Entities;
using BackEnd_Angular.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Angular.Controllers
{
	[ApiController]
	[Route("api/theaters")]
	public class TheatersController: ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

		public TheatersController(ApplicationDbContext context, IMapper mapper)
        {
			this.context = context;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<List<TheaterDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
		{
			var queryable = context.Theaters.AsQueryable();
			await HttpContext.InsertPaginationParametersInHeader(queryable);
			var theaters = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
			return mapper.Map<List<TheaterDTO>>(theaters);
		}
		
		[HttpGet("{id:int}")]
		public async Task<ActionResult<TheaterDTO>> GetById(int id)
		{
			var theater = await context.Theaters.FirstOrDefaultAsync(x => x.Id == id);

			if (theater == null)
			{
				return NotFound();
			}

			return mapper.Map<TheaterDTO>(theater);
		}


		[HttpPost]
		public async Task<ActionResult> Post([FromBody] TheaterCreationDTO theaterCreationDTO)
		{
			var theater = mapper.Map<Theater>(theaterCreationDTO);
			context.Add(theater);
			await context.SaveChangesAsync();
			return NoContent();
		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult> Put(int id, [FromBody] TheaterCreationDTO theaterCreationDTO)
		{
			var theater = await context.Theaters.FirstOrDefaultAsync(x => x.Id == id);

			if (theater == null)
			{
				return NotFound();
			}

			theater = mapper.Map(theaterCreationDTO, theater);

			await context.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			var exist = await context.Theaters.AnyAsync(x => x.Id == id);

			if (!exist)
			{
				return NotFound();
			}

			context.Remove(new Theater() { Id = id });
			await context.SaveChangesAsync();
			return NoContent();
		}
	}
}
