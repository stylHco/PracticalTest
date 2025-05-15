using AutoMapper;

namespace Practical.Features.Employees;

public class EmployeesProfile : Profile
{
    public EmployeesProfile()
    {
        CreateMap<Employee, EmployeeIdentifier>();

        CreateMap<Employee, EmployeesController.EmployeesListModel>();
        
        CreateMap<Employee, EmployeeReferenceModel>();

        CreateMap<EmployeesController.EmployeeCreateModel, Employee>(MemberList.Source);

        CreateMap<EmployeesController.EmployeeUpdateModel, Employee>(MemberList.Source)
            .ReverseMap();
    }
}