using FluentValidation;

namespace MP.ApiDotNet6.Application.DTOs.Validations
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {

            RuleFor(x => x.CodErp)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("Codigo deve ser informado");

            RuleFor(x => x.Name)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Nome deve ser informado");

            RuleFor(x => x.Price)
                    .GreaterThan(0)
                    .WithMessage("Preço deve ser maior que '0'.");
        }
    }
}
