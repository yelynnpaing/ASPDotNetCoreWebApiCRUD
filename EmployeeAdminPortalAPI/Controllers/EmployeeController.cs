using EmployeeAdminPortalAPI.Data;
using EmployeeAdminPortalAPI.Models;
using EmployeeAdminPortalAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var AllEmployees = dbContext.Employees.ToList();
            return Ok(AllEmployees);
        }


        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if(employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }


        [HttpPost]
        public IActionResult AddEmployee(EmployeeDto employeeDto)
        {
            var employeeEntity = new Employee()
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
                Salary = employeeDto.Salary,
            };

            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();
            return Ok(employeeEntity); 
        }


        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, EmployeeDto employeeDto)
        {
            var employee = dbContext.Employees.Find(id);
            if(employee is null)
            {
                return NotFound();
            }
            employee.Name = employeeDto.Name;
            employee.Email = employeeDto.Email;
            employee.Phone = employeeDto.Phone;
            employee.Salary = employeeDto.Salary;

            dbContext.SaveChanges();
            return Ok(employee);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if(employee is null)
            {
                return NotFound();
            }
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
