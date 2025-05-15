using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Practical.Data;
using Practical.Features.Departments;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NJsonSchema.Annotations;
using Practical.Features.Employees;

namespace Practical.Features.Departments;

[ApiController]
[Route("/departments")]

public class DepartmentsController : ControllerBase
{
    

    #region CommonVariablesUsedInThisFile
    private readonly ApplicationDbContext _appDbContext;
    private readonly IMapper _imapper;
    #endregion

    public DepartmentsController(ApplicationDbContext appDbContext, IMapper imapper)
    {
        _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        _imapper = imapper ?? throw new ArgumentNullException(nameof(imapper));
    }
    
    #region ListAllDepartments
    [HttpGet]
    public async Task<ActionResult<List<DepartmentsListModel>>> ListAllDepartments()
    {
        var departments = await _appDbContext.Departments
            .ProjectTo<DepartmentsListModel>(_imapper.ConfigurationProvider)
            .ToListAsync();

        return Ok(departments);
    }

    [JsonSchema(Name = "DepartmentsListModel")]
    public class DepartmentsListModel
    {
        public int Id { get; set; }
    
        [MaxLength(80)]
        public required string  Name { get; set; }
    
        [MaxLength(250)]
        public required string OfficeLocation { get; set; }
    }
    #endregion
    
    #region ListForDropdowns

    [HttpGet("departmentsDropdown")]
    public async Task<IEnumerable<DepartmentReferenceModel>> ListForDropdown()
    {
        return await _appDbContext.Departments
            .ProjectTo<DepartmentReferenceModel>(_imapper.ConfigurationProvider)
            .ToArrayAsync();
    }

    #endregion
    
    #region GetDepartmentById
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DepartmentInfoModel>> GetDepartmentById(int id)
    {
        DepartmentInfoModel? department = await _appDbContext.Departments
            .Where(s => s.Id == id)
            .ProjectTo<DepartmentInfoModel>(_imapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

        if (department == null)
        {
            return NotFound();
        }

        return Ok(department);
    }

    [JsonSchema(Name = "DepartmentInfoModel")]
    public class DepartmentInfoModel
    {
        public int Id { get; set; }
    
        [MaxLength(80)]
        public required string  Name { get; set; }
    
        [MaxLength(250)]
        public required string OfficeLocation { get; set; }
    }
    #endregion
    
    #region CreateNewDepartment

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
    public async Task<ActionResult<int>> Create(DepartmentCreateModel createModel)
    {
        Department department = _imapper.Map<Department>(createModel);

        _appDbContext.Departments.Add(department);
        await _appDbContext.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetDepartmentById), new {id = department.Id}, department.Id);
    }
   
    [JsonSchema(Name = "DepartmentCreateModel")]
    public class DepartmentCreateModel
    {
        
        [MaxLength(80)]
        public required string  Name { get; set; }
    
        [MaxLength(250)]
        public required string OfficeLocation { get; set; }
    }
    
    #endregion
    
    #region UpdateDepartment

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DepartmentUpdateModel>> Update(int id, DepartmentUpdateModel updateModel)
    {
        Department? department = await _appDbContext.Departments
            .SingleOrDefaultAsync(e => e.Id == id);
        if (department == null) return NotFound();

        _imapper.Map(updateModel, department);

        try
        {
            await _appDbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("Concurrency Error occured while updating the record.");
        }

        return Ok(_imapper.Map<DepartmentUpdateModel>(department));

    }
    [JsonSchema(Name = "DepartmentUpdateModel")]
    public class DepartmentUpdateModel
    {
        [MaxLength(80)]
        public required string  Name { get; set; }
    
        [MaxLength(250)]
        public required string OfficeLocation { get; set; }
    }
    #endregion
    
    #region DeleteDepartment
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        Department? department = await _appDbContext.Departments
            .SingleOrDefaultAsync(s => s.Id == id);

        if (department == null)
        {
            return NotFound();
        }

        _appDbContext.Departments.Remove(department);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }
    #endregion
    
    #region GetEmployeesForDepartment

    [HttpGet("{id}/employees")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<EmployeesOfDepartmentModel>>> GetEmployeesByDepartment(int id)
    {
        var department = await _appDbContext.Departments.FirstOrDefaultAsync(d => d.Id == id);

        if (department == null)
            return NotFound($"Department with id {id} not found.");

        var employees = await _appDbContext.Employees
            .Where(e => e.Department.Id == id)
            .ToListAsync();

        var response = employees.Select(e => new EmployeesOfDepartmentModel
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            Salary = e.Salary,
            Name = department.Name
        }).ToList();

        return Ok(response);

    }

    [JsonSchema(Name = "EmployeesOfDepartmentModel")]
    public class EmployeesOfDepartmentModel
    {
    
        [MaxLength(80)]
        public required string  Name { get; set; }        
        public int Id { get; set; }
    
        [MaxLength(80)]
        public required string FirstName { get; set; }
    
        [MaxLength(80)]
        public required string LastName { get; set; }
    
        [MaxLength(255)]
        public required string Email { get; set; }
    
        public required decimal Salary { get; set; }
    }

    #endregion
}