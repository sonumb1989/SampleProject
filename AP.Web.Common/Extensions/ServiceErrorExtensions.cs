using AP.Web.Common.Messages;
using System.Collections.Generic;

namespace AP.Web.Common.Extensions
{
    public static class ServiceErrorExtensions
    {
        /// <summary>
        /// Set validation id to service 
        /// </summary>
        /// <param name="serviceError">ServiceError</param>
        /// <param name="validationId">ValidationId</param>
        public static void AddValidationId(this ServiceError serviceError, string validationId)
        {
            serviceError.Params = serviceError.Params ?? new Dictionary<string, string>();
            serviceError.Params.Add("ValidationId", validationId);
        }
    }
}
