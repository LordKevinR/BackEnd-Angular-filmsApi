using AutoMapper;
using BackEnd_Angular.DTOs.ActorDTOs;
using BackEnd_Angular.DTOs.GenreDTOs;
using BackEnd_Angular.Entities;

namespace BackEnd_Angular.Utilities
{
    public class AutoMapperProfiles: Profile
	{
        public AutoMapperProfiles()
        {
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<GenreCreationDTO, Genre>();
			CreateMap<Actor, ActorDTO>().ReverseMap();
			CreateMap<ActorCreationDTO, Actor>()
                .ForMember(x => x.Photo, options => options.Ignore());
		}
    }
}
