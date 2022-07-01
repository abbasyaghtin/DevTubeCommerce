using DevTubeCommerce.Framework.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Framework.Exceptions
{
    public class AppException : Exception
    {
        public Error Error { get; }

        public AppException(Error error)
            : this(error, null)
        {
        }

        public AppException(Error error, Exception innerException)
            : base(error.EnglishTitle, innerException)
        {
            Error = error;
        }
    }
}
