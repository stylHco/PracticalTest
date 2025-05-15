using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Practical.Data;
using Practical.Features.Departments;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NJsonSchema.Annotations;

namespace Practical.Features.Employees;

[ApiController]
[Route("api/employees")]

public class EmployeesController : ControllerBase
{

    public EmployeesController(ApplicationDbContext appDbContext, IMapper imapper)
    {
        _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        _imapper = imapper ?? throw new ArgumentNullException(nameof(imapper));
    }
    
    #region CommonVariablesUsedInThisFile
    private readonly ApplicationDbContext _appDbContext;
    private readonly IMapper _imapper;
    #endregion
    
    #region ListAllEmployees
    [HttpGet]
    public async Task<ActionResult<List<EmployeesListModel>>> ListAllEmployees()
    {
        var employees = await _appDbContext.Employees
            .ProjectTo<EmployeesListModel>(_imapper.ConfigurationProvider)
            .ToListAsync();

        return Ok(employees);
    }

    [JsonSchema(Name = "EmployeesListModel")]
    public class EmployeesListModel
    {
        public int Id { get; set; }
    
        [MaxLength(80)]
        public required string FirstName { get; set; }
    
        [MaxLength(80)]
        public required string LastName { get; set; }
    
        [MaxLength(255)]
        public required string Email { get; set; }
    
        public required decimal Salary { get; set; }
    
        public required DepartmentReferenceModel Department { get; set; }
    }
    #endregion
    
    #region GetEmployeeById
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeInfoModel>> GetEmployeeById(int id)
    {
        EmployeeInfoModel? employee = await _appDbContext.Employees
            .Where(s => s.Id == id)
            .ProjectTo<EmployeeInfoModel>(_imapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [JsonSchema(Name = "EmployeeInfoModel")]
    public class EmployeeInfoModel
    {
        public int Id { get; set; }
    
        [MaxLength(80)]
        public required string FirstName { get; set; }
    
        [MaxLength(80)]
        public required string LastName { get; set; }
    
        [MaxLength(255)]
        public required string Email { get; set; }
    
        public required decimal Salary { get; set; }
    
        public required DepartmentReferenceModel Department { get; set; }
    }
    #endregion
    
    #region CreateNewEmployee

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
    public async Task<ActionResult<int>> Create(EmployeeCreateModel createModel)
    {
        // check the validity of the department before proceed
        Department? department =
            await _appDbContext.Departments.FirstOrDefaultAsync(d => d.Id == createModel.DepartmentId);
        if (department == null)
        {
            return BadRequest("The selected Department does not exist!");
        }

        Employee employee = _imapper.Map<Employee>(createModel);

        _appDbContext.Employees.Add(employee);
        await _appDbContext.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetEmployeeById), new {id = employee.Id}, employee.Id);
    }
   
    [JsonSchema(Name = "EmployeeCreateModel")]
    public class EmployeeCreateModel
    {
        
        [MaxLength(80)]
        public required string FirstName { get; set; }
    
        [MaxLength(80)]
        public required string LastName { get; set; }
    
        [MaxLength(255)]
        public required string Email { get; set; }
    
        public required decimal Salary { get; set; }
    
        public required int DepartmentId { get; set; }
    }
    
    #endregion
    
    #region UpdateEmployee

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeUpdateModel>> Update(int id, EmployeeUpdateModel updateModel)
    {
        Employee? employee = await _appDbContext.Employees
            .SingleOrDefaultAsync(e => e.Id == id);
        if (employee == null) return NotFound();

        Department? department =
            await _appDbContext.Departments.FirstOrDefaultAsync(d => d.Id == updateModel.DepartmentId);
        if (department == null) return BadRequest("The selected department does not exists!");

        _imapper.Map(updateModel, employee);

        try
        {
            await _appDbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("Concurrency Error occured while updating the record.");
        }

        return Ok(_imapper.Map<EmployeeUpdateModel>(employee));

    }
    [JsonSchema(Name = "EmployeeUpdateModel")]
    public class EmployeeUpdateModel
    {
        
        [MaxLength(80)]
        public required string FirstName { get; set; }
    
        [MaxLength(80)]
        public required string LastName { get; set; }
    
        [MaxLength(255)]
        public required string Email { get; set; }
    
        public required decimal Salary { get; set; }
    
        public required int DepartmentId { get; set; }
    }
    #endregion
    
    #region DeleteEmployee
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        Employee? employee = await _appDbContext.Employees
            .SingleOrDefaultAsync(s => s.Id == id);

        if (employee == null)
        {
            return NotFound();
        }

        _appDbContext.Employees.Remove(employee);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }
    #endregion
    
    #region GetEmployeesWithHighestSalary

    [HttpPut("highest-salary")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<EmployeeTopHighestSalaryModel>> GetTopHighestSalary([FromQuery] int top)
    {
        var topEmployees = await _appDbContext.Employees
            .OrderByDescending(e => e.Salary)
            .Take(top)
            .Select(e => new EmployeeTopHighestSalaryModel
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Salary = e.Salary
            })
            .ToListAsync();

        return Ok(topEmployees);

    }
    [JsonSchema(Name = "EmployeeTopHighestSalaryModel")]
    public class EmployeeTopHighestSalaryModel
    {
        public int Id { get; set; }

        [MaxLength(80)]
        public required string FirstName { get; set; }
    
        [MaxLength(80)]
        public required string LastName { get; set; }
    
        [MaxLength(255)]
    
        public required decimal Salary { get; set; }
    }
    #endregion
    
    #region GetAvgSallaryForDepartment

    [HttpPut("average-salary/{departmentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<DepartmentAvgSalaryModel>> GetDepartmentAvgSalary(int departmentId)
    {
        var department = await _appDbContext.Departments.SingleOrDefaultAsync(d => d.Id == departmentId);
        if (department == null)
            return NotFound($"Selected Department not found.");

        var avgSalary = await _appDbContext.Employees
            .Where(e => e.DepartmentId == departmentId)
            .AverageAsync(e => e.Salary);

        var response = new DepartmentAvgSalaryModel
        {
            DepartmentId = departmentId,
            AverageSalary = avgSalary
        };

        return Ok(response);

    }
    [JsonSchema(Name = "DepartmentAvgSalaryModel")]
    public class DepartmentAvgSalaryModel
    {
        public int DepartmentId { get; set; }
    
        public required decimal AverageSalary { get; set; }
    }
    #endregion
    
    #region GetAvgSallaryForDepartment

    [HttpPut("salary-range")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SalaryRangeEmployeesModel>> GetEmployeesBySalaryRange([FromQuery] decimal min, [FromQuery] decimal max)
    {
        if (min > max)
            return BadRequest("min salary cannot be greater than max salary.");

        var employees = await _appDbContext.Employees
            .Where(e => e.Salary >= min && e.Salary <= max)
            .Select(e => new SalaryRangeEmployeesModel
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Salary = e.Salary
            })
            .ToListAsync();

        return Ok(employees);

    }
    [JsonSchema(Name = "SalaryRangeEmployeesModel")]
    public class SalaryRangeEmployeesModel
    {
        public int Id { get; set; }

        [MaxLength(80)]
        public required string FirstName { get; set; }
    
        [MaxLength(80)]
        public required string LastName { get; set; }
    
        [MaxLength(255)]
    
        public required decimal Salary { get; set; }
    }
    #endregion
}