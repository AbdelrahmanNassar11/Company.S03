using AutoMapper;
using Company.S03.DAL.Models;
using Company.S03.PL.Dtos;

namespace Company.S03.PL.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<CreateDepartmentDto, Department>().ReverseMap();
        }
    }
}
