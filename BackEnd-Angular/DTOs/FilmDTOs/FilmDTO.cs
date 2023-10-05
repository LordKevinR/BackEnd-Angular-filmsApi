using BackEnd_Angular.DTOs.ActorFilmDTOs;
using BackEnd_Angular.DTOs.GenreDTOs;
using BackEnd_Angular.DTOs.TheaterDTOs;

namespace BackEnd_Angular.DTOs.FilmDTOs
{
	public class FilmDTO
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Summary { get; set; }
		public string Trailer { get; set; }
		public bool InTheaters { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string Poster { get; set; }
        public List<GenreDTO> Genres { get; set; }
        public List<ActorFilmDTO> Actors { get; set; }
        public List<TheaterDTO> Theaters { get; set; }
    }
}
