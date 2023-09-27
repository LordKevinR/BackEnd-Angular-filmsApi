using BackEnd_Angular.Entities.RelationshipEntities;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_Angular.Entities
{
	public class Actor
	{
        public int Id { get; set; }
		[Required]
		[StringLength(200)]
		public string Name { get; set; }
		public string Biography { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }

		//navigation properties
        public List<ActorsFilms> ActorsFilms { get; set; }
    }
}
