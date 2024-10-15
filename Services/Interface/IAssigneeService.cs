using Microsoft.AspNetCore.Mvc;

namespace SampleNeo4J.Services.Interface
{
    public interface IAssigneeService
    {
        Task AssigneDepartment(int did, int eid);

    }
}
