using BackEnd_Angular.Validations;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_Angular.DTOs.GenreDTOs
{
    public class GenreCreationDTO
    {
        [Required]
        [StringLength(50)]
        [FirstCapitalLetter]
        public string Name { get; set; }
    }
}
