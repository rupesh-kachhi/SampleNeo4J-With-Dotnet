using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using SampleNeo4J.Models;
using SampleNeo4J.Services.Interface;

namespace SampleNeo4J.Services
{
    public class AssigneeService: IAssigneeService
    {
        private IGraphClient _client;

        public AssigneeService(IGraphClient graphClient)
        {
            _client = graphClient;
        }
        public async Task AssigneDepartment(int did, int eid)
        {
            await _client.Cypher.Match("(d:Department),(e:Employee)").Where((Department d, Employee e) => d.Id == did && e.Id == eid)
                .Create("(d)-[r:hasEmployee]->(e)").ExecuteWithoutResultsAsync();
        }
        public async Task UnassignDepartment(int did, int eid)
        {
            // Match the relationship between the department and the employee
            var relationshipExists = await _client.Cypher
                .Match("(d:Department)-[r:hasEmployee]->(e:Employee)")
                .Where((Department d, Employee e) => d.Id == did && e.Id == eid)
        .Return(r => r.As<object>()) // Return a dummy object to check existence
        .ResultsAsync;

            if (!relationshipExists.Any())
            {
                return ;
            }

            // Delete the relationship
            await _client.Cypher
                .Match("(d:Department)-[r:hasEmployee]->(e:Employee)")
                .Where((Department d, Employee e) => d.Id == did && e.Id == eid)
        .Delete("r") // Delete the relationship
        .ExecuteWithoutResultsAsync();

            return;
        }
    }
}
