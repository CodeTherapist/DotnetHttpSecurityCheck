using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CodeTherapy.HttpSecurityChecks.Core
{
    public static class HttpResponseMessageExtensions
    {

        public static IEnumerable<string> GetHeaderValuesOrEmpty(this HttpResponseMessage httpResponseMessage, string headerName)
        {
            if (httpResponseMessage.Headers.TryGetValues(headerName, out IEnumerable<string> values))
            {
                return values;
            }
            return Array.Empty<string>();
        }

        public static bool TryGetHeaderValue(this HttpResponseHeaders headers, string headerName, out string  value)
        {
            value = null;
            var hasHeader = headers.TryGetValues(headerName, out IEnumerable<string> values);
            if (hasHeader)
            {
                value = values.Single();
            }
            return hasHeader;
        }

    }

}
