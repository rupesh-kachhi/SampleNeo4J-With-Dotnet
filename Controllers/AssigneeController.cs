using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using SampleNeo4J.Models;

namespace SampleNeo4J.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigneeController : Controller
    {
        private IGraphClient _client;

        public AssigneeController(IGraphClient graphClient)
        {
            _client = graphClient;
        }
        [HttpGet("{eid}/assignemployee/{did}/")]
        public async Task<IActionResult> AssigneDepartment(int did, int eid)
        {
            await _client.Cypher.Match("(d:Department),(e:Employee)").Where((Department d, Employee e)=> d.Id == did && e.Id==eid)
                .Create("(d)-[r:hasEmployee]->(e)").ExecuteWithoutResultsAsync();
            return Ok();
        }
        [HttpDelete("{eid}/unassignemployee/{did}/")]
        public async Task<IActionResult> UnassignDepartment(int did, int eid)
        {
            // Match the relationship between the department and the employee
            var relationshipExists = await _client.Cypher
                .Match("(d:Department)-[r:hasEmployee]->(e:Employee)")
                .Where((Department d, Employee e) => d.Id == did &&  e.Id == eid)
        .Return(r => r.As<object>()) // Return a dummy object to check existence
        .ResultsAsync;

            if (!relationshipExists.Any())
            {
                return NotFound("The relationship between the department and employee does not exist.");
            }

            // Delete the relationship
            await _client.Cypher
                .Match("(d:Department)-[r:hasEmployee]->(e:Employee)")
                .Where((Department d, Employee e) => d.Id == did &&  e.Id == eid)
        .Delete("r") // Delete the relationship
        .ExecuteWithoutResultsAsync();

            return Ok(); 
        }
    }
}
