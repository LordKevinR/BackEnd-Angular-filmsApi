using BackEnd_Angular.DTOs.GenreDTOs;
using BackEnd_Angular.DTOs.TheaterDTOs;

namespace BackEnd_Angular.DTOs.FilmDTOs
{
	public class FilmsPostGetDTO
	{
        public List<GenreDTO> Genres { get; set; }
        public List<TheaterDTO> Theaters { get; set; }
    }
}
