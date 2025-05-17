using Application.Features.Departments.Queries;
using Application.Features.Employees.Commands;
using Application.Features.Employees.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class EmployeesController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            return Ok(await Mediator.Send(new GetEmployeeByIdQuery { Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees([FromQuery] GetAllEmployeesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}/department")]
        public async Task<IActionResult> SearchEmployees(int id)
        {
            return Ok(await Mediator.Send(new GetEmployeeByDepartmentQuery { DepartmentId = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Id mismatch");
            }
            
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            return Ok(await Mediator.Send(new DeleteEmployeeCommand { Id = id }));
        }
    }
}
