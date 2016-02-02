using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace Peticion
{
    public class RequestHistory : IRequestHistory
    {
        readonly SQLiteAsyncConnection sqlite;

        public RequestHistory(SQLiteAsyncConnection sqlite)
        {
            this.sqlite = sqlite;
        }

        public async Task AddRequestAsync(HttpRequest request)
        {
            await sqlite.InsertAsync(request);
        }

        public async Task<List<HttpRequest>> GetRequestsAsync()
        {
            return await sqlite.Table<HttpRequest>().ToListAsync();
        }
    }
}
