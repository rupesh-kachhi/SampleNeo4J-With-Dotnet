using Neo4jClient;
using Neo4j.Driver;
using SampleNeo4J.Services.Interface;
using SampleNeo4J.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var client = new BoltGraphClient(new Uri("bolt://localhost:7687"), "neo4j", "12345678");
await client.ConnectAsync();
builder.Services.AddSingleton<IGraphClient>(client);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "/{controller=Home}/{action=Index}");
app.MapControllers();

app.Run();
