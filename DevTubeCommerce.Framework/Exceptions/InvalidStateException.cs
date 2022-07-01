using DevTubeCommerce.Framework.Global;

namespace DevTubeCommerce.Framework.Exceptions
{
    public class InvalidStateException : AppException
    {
        public InvalidStateException(Error error)
           : base(error)
        {
        }

        public InvalidStateException(Error error, Exception innerException)
            : base(error, innerException)
        {
        }
    }
}
