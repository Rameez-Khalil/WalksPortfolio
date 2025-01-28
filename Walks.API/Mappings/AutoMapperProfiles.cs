using AutoMapper;
using Walks.API.Models.Domain;
using Walks.API.Models.DTOs;

namespace Walks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //if the source and destinations have the same names for the props then it will automatically handle it.
             CreateMap<Region, RegionDto>().ReverseMap();
             CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
             CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<UpdateWalkDto, Walk>().ReverseMap();

            

        }
    }

   
}
