using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;

namespace Api.MappingProfiles
{
    public class EmployeeToEmployeeDto : Profile
    {
        public EmployeeToEmployeeDto()
        {
            CreateMap<Employee, GetEmployeeDto>();
        }
    }
}
