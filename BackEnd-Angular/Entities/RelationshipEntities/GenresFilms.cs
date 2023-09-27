namespace BackEnd_Angular.Entities.RelationshipEntities
{
	public class GenresFilms
	{
        public int FilmId { get; set; }
        public int GenreId { get; set; }

		//navigation properties
		public Film Film { get; set; }
        public Genre Genre { get; set; }
    }
}
