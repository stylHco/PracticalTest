using AutoMapper;
namespace Practical.Features.Departments;

public class DepartmentsProfile : Profile
{
    public DepartmentsProfile()
    {
        CreateMap<Department, DepartmentIdentifier>();

        CreateMap<Department, DepartmentsController.DepartmentsListModel>();
        
        CreateMap<Department, DepartmentReferenceModel>();

        CreateMap<DepartmentsController.DepartmentCreateModel, Department>(MemberList.Source);

        CreateMap<DepartmentsController.DepartmentUpdateModel, Department>(MemberList.Source)
            .ReverseMap();
    }
}