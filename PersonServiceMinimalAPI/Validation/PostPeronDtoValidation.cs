using FluentValidation;
using PersonServiceMinimalAPI.Models;

namespace PersonServiceMinimalAPI.Validation {
    public class PostPeronDtoValidation : AbstractValidator<PostPersonDto> {

        public PostPeronDtoValidation() {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Address).NotNull().SetValidator(new PostAddressDtoValidation());
        }
    }
}
