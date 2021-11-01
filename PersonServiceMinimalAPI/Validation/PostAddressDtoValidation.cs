using FluentValidation;
using PersonServiceMinimalAPI.Models;

namespace PersonServiceMinimalAPI.Validation {
    public class PostAddressDtoValidation : AbstractValidator<PostAddressDto> {

        public PostAddressDtoValidation() {
            RuleFor(x => x.City).MaximumLength(100);
            RuleFor(x => x.PostalCode).MaximumLength(6);
            RuleFor(x => x.Province).MaximumLength(100);
            RuleFor(x => x.Street).MaximumLength(100);
            RuleFor(x => x.BuildingNumber).MaximumLength(15);
        }
    }
}
