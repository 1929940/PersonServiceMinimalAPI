using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PersonServiceMinimalAPI.Context;
using PersonServiceMinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(x => x.UseInMemoryDatabase("PersonsDB"));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PersonServiceMinimalAPI", Description = "Docs for PersonServiceMinimalAPI", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PersonServiceMinimalAPI V1");
});


app.MapGet("/persons", ([FromServices] Context context) => {
    return context.Persons.Include(x => x.Address).ToList();
});

app.MapGet("/persons/{id}", ([FromServices] Context context, int id) => {
    var person = context.Persons.Find(id);
    context.Entry(person).Reference(x => x.Address).Load();
    return person is null ? Results.NotFound() : Results.Ok(person);
});

app.MapPost("/persons", ([FromServices] Context context, PostPersonDto personDto) => {
    var person = new Person() {
        FirstName = personDto.FirstName,
        LastName = personDto.LastName,
        DateOfBirth = personDto.DateOfBirth,
        Address = new Address() {
            Street = personDto.Address.Street,
            BuildingNumber = personDto.Address.BuildingNumber,
            City = personDto.Address.City,
            PostalCode = personDto.Address.PostalCode,
            Province = personDto.Address.Province
        }
    };

    context.Add(person);
    context.SaveChanges();

    return Results.Created($"/persons/{person.Id}", person);
});
app.Run();
