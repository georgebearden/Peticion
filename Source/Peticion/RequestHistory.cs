using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace Peticion
{
    public class RequestHistory : IRequestHistory
    {
        readonly SQLiteAsyncConnection sqlite;
        readonly Subject<HttpRequest> requestSubj = new Subject<HttpRequest>();

        public RequestHistory(SQLiteAsyncConnection sqlite)
        {
            this.sqlite = sqlite;
        }

        public async Task AddRequestAsync(HttpRequest request)
        {
            var loaded = await sqlite.Table<HttpRequest>().ToListAsync();
            if (loaded.Any(r => r.Equals(request)))
                return;

            await sqlite.InsertAsync(request);
            requestSubj.OnNext(request);
        }

        public IObservable<HttpRequest> GetRequests()
        {
            return requestSubj.AsObservable();
        }

        public async Task<List<HttpRequest>> GetRequestsAsync()
        {
            return await sqlite.Table<HttpRequest>().ToListAsync();
        }
    }
}
