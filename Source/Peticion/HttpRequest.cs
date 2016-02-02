using System;

namespace Peticion
{
    public class HttpRequest
    {
        public HttpMethods Method { get; set; }

        public string Url { get; set; }

        public override bool Equals(object obj)
        {
            var request = obj as HttpRequest;
            if (request == null)
                return false;

            return Method == request.Method &&
                   string.Compare(Url, request.Url, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
