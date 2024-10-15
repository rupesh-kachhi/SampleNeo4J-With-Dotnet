using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleNeo4J.Controllers;
using SampleNeo4J.Models;

namespace SampleNeo4J.Views.Home
{
    public  class IndexModel : PageModel
    {
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<Department> Departments { get; set; } = new List<Department>();
        public EmployeeController EmployeeController { get; set; }
        public DepartmentController DepartmentController { get; set; }
        public IndexModel(EmployeeController employeeController, DepartmentController departmentController)
        {
            EmployeeController = employeeController;
            DepartmentController = departmentController;
        }
        public void OnGet()
        {
            Employees = EmployeeController.Get().Result;
            Departments = DepartmentController.Get().Result;
        }
    }
}
