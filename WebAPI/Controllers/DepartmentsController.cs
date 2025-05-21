using Application.Features.Departments.Commands;
using Application.Features.Departments.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    public class DepartmentsController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            return Ok(await Mediator.Send(new GetDepartmentByIdQuery { Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments([FromQuery] GetAllDepartmentsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}/salary")]
        public async Task<IActionResult> GetDepartmentSalary(int id)
        {
            return Ok(await Mediator.Send(new GetDepartmentSalaryQuery { DepartmentId = id }));
        }


        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] UpdateDepartmentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Id mismatch");
            }
            
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            return Ok(await Mediator.Send(new DeleteDepartmentCommand { Id = id }));
        }
    }
}
