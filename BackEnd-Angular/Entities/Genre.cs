using BackEnd_Angular.Entities.RelationshipEntities;
using BackEnd_Angular.Validations;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_Angular.Entities
{
	public class Genre
	{
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [FirstCapitalLetter]
        public string Name { get; set; } = null!;

        //navigation properties
        public List<GenresFilms> GenresFilms { get; set; }
    }
}
