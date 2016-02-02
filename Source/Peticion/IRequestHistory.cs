using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peticion
{
    public interface IRequestHistory
    {
        Task AddRequestAsync(HttpRequest request);

        Task<List<HttpRequest>> GetRequestsAsync();
    }
}
