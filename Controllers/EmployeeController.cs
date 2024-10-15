using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using SampleNeo4J.Models;
using SampleNeo4J.Services.Interface;

namespace SampleNeo4J.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private IEmployeeService _IEmployeeService;

        public EmployeeController(IEmployeeService iEmployeeService)
        {
            _IEmployeeService = iEmployeeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Employee employee)
        {
            await _IEmployeeService.Create(employee);
            return Ok();
        }

        [HttpGet]
        public async Task<List<Employee>> Get()
        {
            var employees = await _IEmployeeService.Get();
            return employees;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _IEmployeeService.GetById(id);
            return Ok(employee);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _IEmployeeService.Delete(id);
            return Ok();


        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Employee employee)
        {
            await _IEmployeeService.Update(id, employee);
            return Ok();
        }
    }
}
