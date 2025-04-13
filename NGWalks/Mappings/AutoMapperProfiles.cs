using AutoMapper;
using NGWalks.Models.Domain;
using NGWalks.Models.DTO;

namespace NGWalks.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRquestDto, Region>().ReverseMap();
            CreateMap<AddWalksRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<DifficultyDto, Difficulty>().ReverseMap();  
            CreateMap<UpdateWalksRequestDto, Walk>().ReverseMap();

        }
    }
}
