using AutoMapper;

namespace Fleet.Api.Profiles
{
    public class ContainerProfile : Profile
    {
        public ContainerProfile()
        {
            CreateMap<Models.Container, Dtos.ContainerDto>()
                .ForMember(dest => dest.ContainerId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Dtos.ContainerDto, Models.Container>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ContainerId));
        }
    }
}
