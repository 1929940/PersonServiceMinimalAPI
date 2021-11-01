using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PersonServiceMinimalAPI.Context;
using PersonServiceMinimalAPI.Controller;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(x => x.UseInMemoryDatabase("PersonsDB"));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "PersonServiceMinimalAPI",
        Description = "Docs for PersonServiceMinimalAPI",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PersonServiceMinimalAPI V1");
});

new PersonsController(app).MapEndpoints();

app.Run();
