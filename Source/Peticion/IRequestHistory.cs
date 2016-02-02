using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peticion
{
    public interface IRequestHistory
    {
        Task AddRequestAsync(HttpRequest request);
        IObservable<HttpRequest> GetRequests();
        Task<List<HttpRequest>> GetRequestsAsync();
    }
}
