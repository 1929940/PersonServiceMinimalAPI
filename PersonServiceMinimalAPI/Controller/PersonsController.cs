using PersonServiceMinimalAPI.Models;
using Microsoft.EntityFrameworkCore;
using PersonServiceMinimalAPI.Context;

namespace PersonServiceMinimalAPI.Controller {
    public class PersonsController {
        private readonly WebApplication _app;

        public PersonsController(WebApplication app) {
            _app = app;
        }

        public void MapEndpoints() {
            _app.MapGet("/persons", GetPersons);
            _app.MapGet("/persons/{id}", GetPerson);
            _app.MapPost("/persons", PostPerson);
        }

        public IResult GetPersons(DataContext context) {
            var res = context.Persons.Include(x => x.Address).ToList();
            return Results.Ok(res);
        }

        public IResult GetPerson(DataContext context, int id) {
            var person = context.Persons.Find(id);
            if (person == null)
                return Results.NotFound();

            context.Entry(person).Reference(x => x.Address).Load();
            return Results.Ok(person);
        }

        public IResult PostPerson(DataContext context, PostPersonDto personDto) {
            if (string.IsNullOrEmpty(personDto.FirstName))
                return Results.BadRequest("FirstName is required.");

            if (string.IsNullOrEmpty(personDto.LastName)) 
                return Results.BadRequest("LastName is required.");

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
        }
    }
}
