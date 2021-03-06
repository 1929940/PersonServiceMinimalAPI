using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PersonServiceMinimalAPI.Context;
using PersonServiceMinimalAPI.Installer;
using PersonServiceMinimalAPI.Models;

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

builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<PostPersonDto>());

var app = builder.Build();
new Installer(app).Install();


app.UseSwagger();

app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PersonServiceMinimalAPI V1");
});

app.Run();
