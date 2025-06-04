using AutoMapper;
using OrgManagement.Entities.Models;

namespace OrgManagement.API.Mapper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Organization, OrganizationDto>()
            .ForMember(dest => dest.SubOrganizations, opt => opt.MapFrom(src => src.SubOrganizations));
        CreateMap<Person, PersonDto>();
        CreateMap<Person, PersonCreateDto>().ReverseMap();
    }
}