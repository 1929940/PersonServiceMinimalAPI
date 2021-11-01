using PersonServiceMinimalAPI.Models;
using Microsoft.EntityFrameworkCore;
using PersonServiceMinimalAPI.Context;
using PersonServiceMinimalAPI.Installer;
using FluentValidation;

namespace PersonServiceMinimalAPI.Controller {
    public class PersonsController : InstallableBase {
        public PersonsController(WebApplication app) : base(app) {
        }

        public override void Install() {
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

        public IResult PostPerson(
            DataContext context,
            PostPersonDto personDto,
            IValidator<PostPersonDto> validator) {

            var validationResult = validator.Validate(personDto);

            if (validationResult.IsValid == false) {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage);
                return Results.BadRequest(errors);
            }

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
