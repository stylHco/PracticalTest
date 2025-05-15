using System.ComponentModel.DataAnnotations;

namespace Practical.Features.Departments;

public record DepartmentIdentifier
{
    public required int Id { get; init; }

    public static implicit operator DepartmentIdentifier(Department department) => new()
    {
        Id = department.Id,
    };
}
public class Department
{
    public Department() {}
  
    public Department(DepartmentIdentifier identifier)
    {
        Id = identifier.Id;
    }
    
    public int Id { get; set; }
    
    [MaxLength(80)]
    public required string  Name { get; set; }
    
    [MaxLength(250)]
    public required string OfficeLocation { get; set; }
}