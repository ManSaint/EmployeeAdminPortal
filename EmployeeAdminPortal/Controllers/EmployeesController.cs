using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly ApplicationDbContext _dbcontext;

    public EmployeesController(ApplicationDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    [HttpGet]
    public IActionResult GetAllEmployees()
    {
        var allEmployees = _dbcontext.Employees.ToList();

        return Ok(allEmployees);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetEmployeeById(Guid id)
    {
        var employee = _dbcontext.Employees.Find(id);

        if (employee is null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpPost]
    public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
    {
        var employeeEntity = new Employee()
        {
            Name = addEmployeeDto.Name,
            Email = addEmployeeDto.Email,
            Phone = addEmployeeDto.Phone,
            Salary = addEmployeeDto.Salary
        };

        _dbcontext.Employees.Add(employeeEntity);
        _dbcontext.SaveChanges();

        return Ok(employeeEntity);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
    {
        var employee = _dbcontext.Employees.Find(id);

        if (employee is null)
        {
            return NotFound();
        }

        employee.Name = updateEmployeeDto.Name;
        employee.Email = updateEmployeeDto.Email;
        employee.Phone = updateEmployeeDto.Phone;
        employee.Salary = updateEmployeeDto.Salary;

        _dbcontext.SaveChanges();

        return Ok(employee);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteEmployee(Guid id)
    {
        var employee = _dbcontext.Employees.Find(id);

        if (employee is null)
        {
            return NotFound();
        }

        _dbcontext.Employees.Remove(employee);
        _dbcontext.SaveChanges();

        return Ok();
    }
}
