using AutoMapper;
using Api.V1.Dtos;
using Api.Models;
using Api.Helpers;

namespace Api.V1.Profiles
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<Employee, EmployeeDto>()
            .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome}")
                )
                .ForMember(
                    dest => dest.Idade,
                    opt => opt.MapFrom(src => src.DataDeNascimento.GetCurrentAge())
                );

            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeeRegisterDto>().ReverseMap();
            CreateMap<Employee, EmployeePatchDto>().ReverseMap();
        }
    }
}