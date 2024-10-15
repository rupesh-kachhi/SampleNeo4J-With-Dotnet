using Microsoft.AspNetCore.Mvc;
using SampleNeo4J.Models;

namespace SampleNeo4J.Services.Interface
{
    public interface IEmployeeService
    {
        Task<Employee> Create(Employee employee);
        Task<List<Employee>> Get();
        Task<Employee> GetById(int id);
        Task Delete(int id);
        Task<Employee> Update(int id, Employee employee);

    }
}
