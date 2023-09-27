namespace BackEnd_Angular.Entities.RelationshipEntities
{
	public class ActorsFilms
	{
        public int FilmId { get; set; }
        public int ActorId { get; set; }

		//navigation properties
		public Film Film { get; set; }
        public Actor Actor { get; set; }

        // extra properties
        public string Character { get; set; }
        public int Order { get; set; } 
    }
}
