using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace SiteMercado.Domain
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(x => x.Nome)
               .NotEmpty().WithMessage("O nome do produto deve ser informado.")
               .MaximumLength(100).WithMessage("O nome do produto deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Valor)
               .GreaterThanOrEqualTo(0).WithMessage("O valor do produto deve ser maior do que 0.");

            //RuleFor(x => x.Imagem)
            //    .MaximumLength(2)
            //    .When(x => !string.IsNullOrEmpty(x.Imagem));
        }

        protected override void EnsureInstanceNotNull(object produto)
        {
            if (produto == null)
            {
                var error = new ValidationFailure("Produto", "O Produto deve ser informado.", null);

                throw new ValidationException("Aconteceu um problema nas validações do produto.", new List<ValidationFailure> { error });
            }
        }
    }
}
