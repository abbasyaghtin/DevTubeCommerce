using DevTubeCommerce.Framework.Global;

namespace DevTubeCommerce.Framework.Exceptions
{
    public class BusinessRuleException : AppException
    {
        public BusinessRuleException(Error error) : base(error)
        {
        }

        public BusinessRuleException(Error error, Exception innerException) : base(error, innerException)
        {
        }
    }
}
