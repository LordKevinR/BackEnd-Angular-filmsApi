using AutoMapper;
using BackEnd_Angular.DTOs.ActorDTOs;
using BackEnd_Angular.DTOs.GenreDTOs;
using BackEnd_Angular.DTOs.TheaterDTOs;
using BackEnd_Angular.Entities;
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
		}
    }
}
