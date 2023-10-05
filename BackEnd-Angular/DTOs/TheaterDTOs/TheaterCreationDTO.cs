using System.ComponentModel.DataAnnotations;

namespace BackEnd_Angular.DTOs.TheaterDTOs
{
	public class TheaterCreationDTO
	{
		[Required]
		[StringLength(75)]
		public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}
