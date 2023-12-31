using Api.BusinessLogic;
using Api.Interfaces;
using Api.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee Benefit Cost Calculation Api",
        Description = "Api to support employee benefit cost calculations"
    });
});

var allowLocalhost = "allow localhost";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowLocalhost,
        policy => { policy.WithOrigins("http://localhost:3000", "http://localhost"); });
});

// why: Decided to use Automapper for faster development of higher quality conversion functionality
builder.Services.AddAutoMapper(typeof(Program));

// why: Using Steve Smith's variation of clean architecture where interfaces are used with dependency injection.
// Usually core functionality, business logic, and interfaces are separated into different projects from implementations and head projects
// This yeilds:
//   -Better separation of concerns
//   -Increased testability
//   -Increased maintainability
//   -Increased readability

builder.Services.AddTransient<IDependentRepository, InMemoryDependentRepository>();
builder.Services.AddTransient<IEmployeeRepository, InMemoryEmployeeRepository>();
builder.Services.AddTransient<IDeduction, BaseBenefitDeduction>();
builder.Services.AddTransient<IDeduction, DependentBenefitDeduction>();
builder.Services.AddTransient<IDeduction, HighSalaryDeduction>();
builder.Services.AddTransient<IDeduction, OlderDependentsDeduction>();
builder.Services.AddTransient<IPaycheckCalculator, BiweeklyPaycheckCalculator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowLocalhost);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
