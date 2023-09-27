namespace BackEnd_Angular.Entities.RelationshipEntities
{
	public class TheatersFilms
	{
        public int FilmId { get; set; }
        public int TheaterId { get; set; }

		//navigation properties
		public Film Film { get; set; }
        public Theater Theater { get; set; }
    }
}
