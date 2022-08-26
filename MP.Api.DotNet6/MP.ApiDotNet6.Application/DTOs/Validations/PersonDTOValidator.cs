using FluentValidation;

namespace MP.ApiDotNet6.Application.DTOs.Validations
{
    public class PersonDTOValidator : AbstractValidator<PersonDTO>
    {
        public PersonDTOValidator()
        {
            RuleFor(x => x.Document)
                .NotEmpty()
                .NotNull()
                .WithMessage("Documento deve ser informado");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nome deve ser informado");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .NotNull()
                .WithMessage("Telefone deve ser informado");
        }
    }
}
