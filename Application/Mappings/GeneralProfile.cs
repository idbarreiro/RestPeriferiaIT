using Application.DTOs;
using Application.Features.Departments.Commands;
using Application.Features.Employees.Commands;
using Application.Features.Users.Commands;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region DTOs
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<User, UserDto>();
            #endregion

            #region Commands
            CreateMap<CreateEmployeeCommand, Employee>();
            CreateMap<CreateDepartmentCommand, Department>();
            CreateMap<CreateUserCommand, User>();
            #endregion
        }
    }
}
