using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using SampleNeo4J.Models;

namespace SampleNeo4J.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private IGraphClient _client;

        public EmployeeController(IGraphClient graphClient)
        {
            _client = graphClient;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Employee employee)
        {
            await _client.Cypher.Create("(d:Employee $employee)").WithParam("dept", employee).ExecuteWithoutResultsAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _client.Cypher.Match("(n:Employee)").Return(n => n.As<Employee>()).ResultsAsync;
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _client.Cypher.Match("(d:Employee)")
                .Where((Employee d) => d.id == id)
                .Return(d => d.As<Employee>())
                .ResultsAsync;
            return Ok(employee.LastOrDefault());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _client.Cypher.Match("(d:Employee)")
               .Where((Employee d) => d.id == id)
               .Delete("d").ExecuteWithoutResultsAsync();
            return Ok();


        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Employee employee)
        {
            await _client.Cypher.Match("(d:Employee)")
                .Where((Employee d) => d.id == id)
                .Set("d = $dept")
                .WithParam("dept", employee)
                .ExecuteWithoutResultsAsync();
            return Ok();
        }
    }
}
