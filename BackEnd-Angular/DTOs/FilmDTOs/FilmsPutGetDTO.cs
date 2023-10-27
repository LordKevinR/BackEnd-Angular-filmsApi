using BackEnd_Angular.DTOs.ActorFilmDTOs;
using BackEnd_Angular.DTOs.GenreDTOs;
using BackEnd_Angular.DTOs.TheaterDTOs;

namespace BackEnd_Angular.DTOs.FilmDTOs
{
	public class FilmsPutGetDTO
	{
        public FilmDTO Film { get; set; }
        public List<GenreDTO> SelectedGenres { get; set; }
        public List<GenreDTO> NoSelectedGenres { get; set; }
        public List<TheaterDTO> SelectedTheaters { get; set; }
        public List<TheaterDTO> NoSelectedTheaters { get; set; }
        public List<ActorFilmDTO> Actors { get; set; }
    }
}
