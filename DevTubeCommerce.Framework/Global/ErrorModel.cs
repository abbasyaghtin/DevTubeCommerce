using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Framework.Global
{
    public class ErrorModel
    {
        public int Code { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Description { get; set; }
        public string StackTrace { get; set; }
    }
    public class ResponseModel
    {
        public static DataResponseModel<TData> FromData<TData>(TData data) => new DataResponseModel<TData> { Data = data };
        public static ErrorResponseModel FromError(ErrorModel error) => new ErrorResponseModel { Error = error };

    }
    public class ErrorResponseModel : ResponseModel
    {
        public ErrorModel Error { get; set; }
    }
    public class DataResponseModel<TData> : ResponseModel
    {
        public TData Data { get; set; }
    }
}
