using BackEnd_Angular.Entities.RelationshipEntities;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_Angular.Entities
{
	public class Theater
	{
        public int Id { get; set; }
        [Required]
        [StringLength(75)]
        public string Name { get; set; }
        public Point Location { get; set; }

		//navigation properties
		public List<TheatersFilms> TheatersFilms { get; set; }
    }
}
