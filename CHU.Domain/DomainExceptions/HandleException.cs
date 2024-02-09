using CHU.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHU.Domain.DomainExceptions
{
    public class HandleException
    {
        public int statusCode { get; set; }
        public ErroResponse erro { get; set; }
    }
}
