using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using SampleNeo4J.Models;

namespace SampleNeo4J.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigneController : Controller
    {
        private IGraphClient _client;

        public AssigneController(IGraphClient graphClient)
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
    }
}
