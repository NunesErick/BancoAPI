using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHU.Domain.Requests
{
    public class ExtratoRequestValidator : AbstractValidator<ExtratoRequest>
    {
        public ExtratoRequestValidator()
        {
            RuleFor(request => request.Id).NotEmpty().WithMessage("O campo Id é obrigatório.");
            RuleFor(request => request.DtInicial).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data inicial deve ser menor ou igual à data atual.");
            RuleFor(request => request.DtFinal).GreaterThanOrEqualTo(request => request.DtInicial).WithMessage("A data final deve ser maior ou igual à data inicial.");
        }
    }

    public class ExtratoRequest
    {
        public Guid Id { get; set; }
        public DateTime? DtInicial { get; set; }
        public DateTime? DtFinal { get; set; }
    }
}
