using BackEnd_Angular.Entities.RelationshipEntities;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_Angular.Entities
{
	public class Film
	{
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public bool InTheaters { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }

        //navigation properties
        public List<ActorsFilms> ActorsFilms { get; set; }
        public List<GenresFilms> GenresFilms { get; set; }
        public List<TheatersFilms> TheatersFilms { get; set; }
    }
}
