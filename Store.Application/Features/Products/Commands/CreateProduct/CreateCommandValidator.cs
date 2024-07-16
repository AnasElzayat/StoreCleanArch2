using FluentValidation;

namespace Store.Application.Features.Products.Commands.CreateProduct
{
    public class CreateCommandValidator :AbstractValidator<CreateProductCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(p => p.CatId).NotEmpty().NotNull();
        }
    }
}
