using AutoMapper;
using Fleet.Domain;
using Fleet.Dtos;
using Fleet.Domain.Enums;

namespace Fleet.Api.Profiles
{
    public class TransportationProfile : Profile
    {
        public TransportationProfile()
        {
            CreateMap<Transportation, TransportationDto>()
               .ForMember(dest => dest.TransportationId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Containers, opt => opt.MapFrom(src => src.LoadContainers));

            CreateMap<TransportationDto, Transportation>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TransportationId))
                .ForMember(dest => dest.LoadContainers, opt => opt.MapFrom(src => src.Containers));

            CreateMap<TransportationType, TransportationTypeDto>();
        }
    }
}
