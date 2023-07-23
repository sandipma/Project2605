using BaseModels.ApplicationUser;
using BaseModels.Employee;
using BaseRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Project2605.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;

        private readonly IConfiguration _config;

        public EmployeeController(IConfiguration config, IEmployee employee)
        {
            _config = config;
            _employee = employee;

        }

        [HttpPost("EmplyeeUpsert")]
        public async Task<ActionResult<string>> EmployeeAddUpdate([FromForm] EmployeeInsert empDetails)

        {
            if (empDetails.EmployeeId == null)
            {
                var addedUser = await _employee.AddEmployeeAsync(empDetails);

                if (addedUser != null)
                {
                    return Ok("Employee Added Successfully");
                }
                return BadRequest(addedUser);

            }
            else
            {
                var updatedUser = await _employee.UpdateEmployeeAsync
                    (empDetails);

                if (updatedUser != null)
                {
                    return Ok("Employee updated Successfully");
                }
                return BadRequest(updatedUser);

            }

        }


        [HttpGet("GetAllEmployee/{applicationUserId}")]

        public async Task<ActionResult<List<EmplyeeData>>> GetAllEmployees(int applicationUserId)
        {

            var employess = await _employee.GetEmployeeListAsync(applicationUserId);

            if (employess != null)
            {
                return Ok(employess);
            }
            else
            {

                return BadRequest("No Employees Found");
            }

        }

        [HttpGet("GetEmployee/{employeeId}")]

        public async Task<ActionResult<IEnumerable<EmplyeeData>>> GetEmployee(int employeeId)
        {

            var employee = await _employee.GetEmployeeListAsync(employeeId);

            if (employee != null)
            {
                return Ok(employee);
            }
            else
            {

                return BadRequest("Employee Details Not Found" + employeeId);
            }

        }

        [HttpDelete("Employee/{employeeId}")]

        public async Task<ActionResult> DeleteEmployee(int employeeId)
        {
            var employeetoDeleted = await _employee.GetEmplyeeByIdAsync(employeeId);

            if (employeetoDeleted != null)
            {
                var employee = await _employee.DeleteEmployeeAsync(employeeId);
                
                return Ok("Employee Deleted Sccuessfully" + employee);

            }

            return BadRequest("Employee Details Not Found" + employeeId);

        }

    }
}

