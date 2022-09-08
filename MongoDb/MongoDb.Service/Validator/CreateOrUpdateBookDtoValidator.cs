using FluentValidation;
using MongoDb.Domain.Dto;

namespace MongoDb.Service.Validator
{
    public class CreateOrUpdateBookDtoValidator : AbstractValidator<CreateOrUpdateBookDto>
    {
        public CreateOrUpdateBookDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Book Name is required");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Provide a brief description about the book");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");

            RuleFor(x => x.AuthorName).NotEmpty().WithMessage("Author name is required");
        }
    }
}
