using Api.Dtos.Dependent;
using Api.Models;
using AutoMapper;

namespace Api.MappingProfiles
{
    public class DependentToDependentDto : Profile
    {
        public DependentToDependentDto()
        {
            CreateMap<Dependent, GetDependentDto>();
        }
    }
}
