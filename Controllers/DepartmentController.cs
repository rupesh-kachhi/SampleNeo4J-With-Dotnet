using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using SampleNeo4J.Models;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : Controller
{
    private IGraphClient _client;

    public DepartmentController(IGraphClient graphClient) 
    {
        _client = graphClient;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Department dept)
    {
        await _client.Cypher.Create("(d:Department $dept)").WithParam("dept", dept).ExecuteWithoutResultsAsync();
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var departments = await _client.Cypher.Match("(n:Department)").Return(n => n.As<Department>()).ResultsAsync;
        return Ok(departments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var department = await _client.Cypher.Match("(d:Department)")
            .Where((Department d) => d.id == id)
            .Return(d => d.As<Department>())
            .ResultsAsync;
        return Ok(department.LastOrDefault());
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _client.Cypher.Match("(d:Department)")
           .Where((Department d) => d.id == id)
           .Delete("d").ExecuteWithoutResultsAsync();
        return Ok();
                

    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update (int id,[FromBody] Department dept)
    {
        await _client.Cypher.Match("(d:Department)")
            .Where((Department d) => d.id == id)
            .Set("d = $dept")
            .WithParam("dept", dept)
            .ExecuteWithoutResultsAsync();
        return Ok();
    }
}
