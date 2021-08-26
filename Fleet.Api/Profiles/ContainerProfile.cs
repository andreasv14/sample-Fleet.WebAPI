using AutoMapper;
using Fleet.Domain;

namespace Fleet.Api.Profiles
{
    public class ContainerProfile : Profile
    {
        public ContainerProfile()
        {
            CreateMap<Container, Dtos.ContainerDto>()
                .ForMember(dest => dest.ContainerId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Dtos.ContainerDto, Container>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ContainerId));
        }
    }
}
