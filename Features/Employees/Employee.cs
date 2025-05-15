using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practical.Features.Departments;

namespace Practical.Features.Employees;
public record EmployeeIdentifier
{
    public required int Id { get; init; }

    public static implicit operator EmployeeIdentifier(Employee employee) => new()
    {
        Id = employee.Id,
    };
}

public class Employee
{
    public Employee() {}

    public Employee(EmployeeIdentifier identifier)
    {
        Id = identifier.Id;
    }
    
    public int Id { get; set; }
    
    [MaxLength(80)]
    public required string FirstName { get; set; }
    
    [MaxLength(80)]
    public required string LastName { get; set; }
    
    [MaxLength(255)]
    public required string Email { get; set; }
    
    public required decimal Salary { get; set; }

    public required Department Department { get; set; } = null!;
    public int DepartmentId { get; set; }

}

public class DeviceEntityConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(d => d.Salary)
            .HasPrecision(15, 3);

        builder.HasIndex(entity => entity.Email)
            .IsUnique();
    }
}
