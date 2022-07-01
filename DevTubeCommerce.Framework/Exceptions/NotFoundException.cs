using DevTubeCommerce.Framework.Global;

namespace DevTubeCommerce.Framework.Exceptions
{
    public class NotFoundException : AppException
    {

        public NotFoundException(Error error)
          : base(error)
        {
        }

        public NotFoundException(Error error, Exception innerException)
            : base(error, innerException)
        {
        }
    }
}
