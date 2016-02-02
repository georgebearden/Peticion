using System.Collections.Generic;

namespace Peticion
{
    public class HttpRequest
    {
        public HttpMethods Method { get; set; }

        public string Url { get; set; }
    }
}
