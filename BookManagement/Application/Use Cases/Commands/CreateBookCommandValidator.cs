using FluentValidation;

namespace Application.Use_Cases.Commands
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Author).NotEmpty().MaximumLength(100);
            RuleFor(x => x.ISBN).NotEmpty().MaximumLength(13);
            RuleFor(x => x.PublicationDate).NotEmpty();
        }
    }
}
