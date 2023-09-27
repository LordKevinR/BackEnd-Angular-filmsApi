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
    }
}
