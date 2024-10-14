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
            await _client.Cypher
        .Create("(e:Employee {id: $id, name: $name, skills: $skills, level: $level})")
        .WithParams(new
        {
            id = employee.Id,
            name = employee.Name,
            skills = employee.Skills, // Assuming Skills is a List<string>
            level = employee.Level
        })
        .ExecuteWithoutResultsAsync();
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
                .Where((Employee d) => d.Id == id)
                .Return(d => d.As<Employee>())
                .ResultsAsync;
            return Ok(employee.LastOrDefault());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _client.Cypher.Match("(d:Employee)")
               .Where((Employee d) => d.Id == id)
               .Delete("d").ExecuteWithoutResultsAsync();
            return Ok();


        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Employee employee)
        {
            await _client.Cypher.Match("(d:Employee)")
                .Where((Employee d) => d.Id == id)
                .Set("d = $dept")
                .WithParam("dept", employee)
                .ExecuteWithoutResultsAsync();
            return Ok();
        }
    }
}
