using CHU.Domain.DomainExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHU.Infrastructure.Interfaces
{
    public interface IUtil
    {
        bool VerificaSeEDiaUtil(DateTime date);
        HandleException HandleException(Exception ex);
    }
}
