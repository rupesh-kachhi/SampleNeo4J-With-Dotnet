using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using SampleNeo4J.Models;
using SampleNeo4J.Services.Interface;

namespace SampleNeo4J.Services
{
    public class EmployeeService: IEmployeeService
    {
        private IGraphClient _client;

        public EmployeeService(IGraphClient graphClient)
        {
            _client = graphClient;
        }
        public async Task<Employee> Create(Employee employee)
        {
           var emp = await _client.Cypher.Create("(d:Employee $employee)").WithParam("employee", employee).Return(e => e.As<Employee>())
        .ResultsAsync;
            return emp.LastOrDefault() ;
           
        }
        public async Task<List<Employee>> Get()
        {
            var employees = await _client.Cypher.Match("(n:Employee)").Return(n => n.As<Employee>()).ResultsAsync;
            return employees.ToList();
        }
        public async Task<Employee> GetById(int id)
        {
            var employee = await _client.Cypher.Match("(d:Employee)")
                .Where((Employee d) => d.Id == id)
                .Return(d => d.As<Employee>())
                .ResultsAsync;
            return employee.LastOrDefault();
        }
        public async Task Delete(int id)
        {
            await _client.Cypher.Match("(d:Employee)")
               .Where((Employee d) => d.Id == id)
               .Delete("d").ExecuteWithoutResultsAsync();


        }
        public async Task<Employee> Update(int id, Employee employee)
        {
          var emp =  await _client.Cypher.Match("(d:Employee)")
                .Where((Employee d) => d.Id == id)
                .Set("d = $dept")
                .WithParam("dept", employee)
                .Return(d => d.As<Employee>())
        .ResultsAsync; ;
            return emp.FirstOrDefault() ;
        }
    }
}
