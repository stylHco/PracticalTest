using System.ComponentModel.DataAnnotations;
using Practical.Features.Departments;

namespace Practical.Features.Departments;

public class DepartmentReferenceModel
{
    [MaxLength(80)]
    public required string  Name { get; set; }    
}