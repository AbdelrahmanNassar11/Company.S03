using AutoMapper;
using Company.S03.DAL.Models;
using Company.S03.PL.Dtos;

namespace Company.S03.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // دي اما يكون الايامي مختلفه بين الكلاسين وده هيتم تعديلها
            //.ForMember(e => e.Name, o => o.MapFrom(s => s.Name))
            CreateMap<CreateEmployeeDto, Employee>().ForMember(e => e.Name,o => o.MapFrom(s => s.Name)).ReverseMap();
            //CreateMap<Employee, CreateEmployeeDto>();
        }
    }
}
