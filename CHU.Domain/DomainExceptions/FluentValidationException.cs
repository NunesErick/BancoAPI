using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHU.Domain.DomainExceptions
{
    public class FluentValidationException : Exception
    {
        public List<FluentValidation.Results.ValidationFailure> ValidationFailures { get; }

        public FluentValidationException(List<FluentValidation.Results.ValidationFailure> validationFailures)
        {
            ValidationFailures = validationFailures;
        }

        public override string Message
        {
            get
            {
                // Aqui, você pode criar uma string combinando todas as mensagens de erro.
                var errorMessage = new StringBuilder();
                foreach (var failure in ValidationFailures)
                {
                    errorMessage.AppendLine($"{failure.PropertyName}: {failure.ErrorMessage}");
                }

                return errorMessage.ToString();
            }
        }
    }
}
