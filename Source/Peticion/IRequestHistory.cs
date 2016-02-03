using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peticion
{
    public interface IRequestHistory
    {
        Task AddRequestAsync(HttpRequest request);
        IObservable<HttpRequest> GetRequestsObservable();
        Task<List<HttpRequest>> GetRequestsAsync();
    }
}
