using Practical.Features.Departments;

namespace Practical.Features.Employees;

public class EmployeeReferenceModel
{
    public int Id { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string Email { get; set; }
    
    public required Department Department { get; set; }
}