using AutoMapper;
using BackEnd_Angular.DTOs.ActorDTOs;
using BackEnd_Angular.DTOs.ActorFilmDTOs;
using BackEnd_Angular.DTOs.FilmDTOs;
using BackEnd_Angular.DTOs.GenreDTOs;
using BackEnd_Angular.DTOs.TheaterDTOs;
using BackEnd_Angular.Entities;
using BackEnd_Angular.Entities.RelationshipEntities;
using NetTopologySuite.Geometries;

namespace BackEnd_Angular.Utilities
{
    public class AutoMapperProfiles: Profile
	{
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<GenreCreationDTO, Genre>();
			CreateMap<Actor, ActorDTO>().ReverseMap();
			CreateMap<ActorCreationDTO, Actor>()
                .ForMember(x => x.Photo, options => options.Ignore());

            CreateMap<TheaterCreationDTO, Theater>()
                .ForMember(x => x.Location, x => x.MapFrom(dto =>
                geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));

            CreateMap<Theater, TheaterDTO>()
                .ForMember(x => x.Latitude, dto => dto.MapFrom(field => field.Location.Y))
                .ForMember(x => x.Longitude, dto => dto.MapFrom(field => field.Location.X));

            CreateMap<FilmCreationDTO, Film>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.GenresFilms, options => options.MapFrom(MapGenresFilms))
                .ForMember(x => x.TheatersFilms, options => options.MapFrom(MapTheatersFilms))
                .ForMember(x => x.ActorsFilms, options => options.MapFrom(MapActorsFilms));

            CreateMap<Film, FilmDTO>()
                .ForMember(x => x.Genres, options => options.MapFrom(MapGenresFilms))
                .ForMember(x => x.Actors, options => options.MapFrom(MapActorsFilms))
                .ForMember(x => x.Theaters, options => options.MapFrom(MapTheaterFilms));
		}

        private List<TheaterDTO> MapTheaterFilms(Film film, FilmDTO filmDTO)
        {
            var result = new List<TheaterDTO>();

            if(film.TheatersFilms != null)
            {
                foreach (var theaterFilms in film.TheatersFilms)
                {
                    result.Add(new TheaterDTO()
                    {
                       Id = theaterFilms.TheaterId,
                       Name = theaterFilms.Theater.Name,
                       Latitude = theaterFilms.Theater.Location.Y,
                       Longitude = theaterFilms.Theater.Location.X
                    });
                }
            }

			return result;
		}
    
        private List<ActorFilmDTO> MapActorsFilms(Film film, FilmDTO filmDTO)
        {
            var result = new List<ActorFilmDTO>();

            if(film.ActorsFilms != null)
            {
                foreach (var actorFilms in film.ActorsFilms)
                {
                    result.Add(new ActorFilmDTO()
                    {
                        Id = actorFilms.ActorId,
                        Name = actorFilms.Actor.Name,
                        Photo = actorFilms.Actor.Photo,
                        Order = actorFilms.Order,
                        Character = actorFilms.Character
                    });
                }
            }

			return result;
		}
        
        private List<GenreDTO> MapGenresFilms(Film film, FilmDTO filmDTO)
        {
            var result = new List<GenreDTO>();

            if(film.GenresFilms != null)
            {
                foreach (var genre in film.GenresFilms)
                {
                    result.Add(new GenreDTO() { Id = genre.GenreId, Name = genre.Genre.Name });
                }
            }

			return result;
		}

		private List<GenresFilms> MapGenresFilms(FilmCreationDTO filmCreationDTO, Film film)
        {
            var result = new List<GenresFilms>();

            if(filmCreationDTO.GenresIds == null)
            {
                return result;
            }

            foreach (var id in filmCreationDTO.GenresIds)
            {
                result.Add(new GenresFilms() { GenreId = id });
            }

            return result;
        }

        private List<TheatersFilms> MapTheatersFilms(FilmCreationDTO filmCreationDTO, Film film)
        {
            var result = new List<TheatersFilms>();

            if(filmCreationDTO.TheatersIds == null)
            {
                return result;
            }

            foreach (var id in filmCreationDTO.TheatersIds)
            {
                result.Add(new TheatersFilms() { TheaterId = id });
            }

            return result;
        }

        private List<ActorsFilms> MapActorsFilms(FilmCreationDTO filmCreationDTO, Film film)
        {
            var result = new List<ActorsFilms>();

            if(filmCreationDTO.Actors == null)
            {
                return result;
            }

            foreach (var actor in filmCreationDTO.Actors)
            {
                result.Add(new ActorsFilms() { ActorId = actor.Id, Character = actor.Character });
            }

            return result;
        }
    }
}
