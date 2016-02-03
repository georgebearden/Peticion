using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace Peticion
{
    public interface ISQLiteAsyncConnection
    {
        Task InsertAsync(HttpRequest request);
        Task<List<T>> Get<T>() where T : class;
    }

    internal class SQLiteAsyncConnectionImpl : ISQLiteAsyncConnection
    {
        readonly SQLiteAsyncConnection sqlite;

        public SQLiteAsyncConnectionImpl(SQLiteAsyncConnection sqlite)
        {
            this.sqlite = sqlite;
        }

        public async Task InsertAsync(HttpRequest request)
        {
            await sqlite.InsertAsync(request);
        }

        public async Task<List<T>> Get<T>() where T : class
        {
            return await sqlite.Table<T>().ToListAsync();
        }
    }
}
