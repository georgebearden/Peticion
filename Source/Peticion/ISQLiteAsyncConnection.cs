using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace Peticion
{
    public interface ISQLiteAsyncConnection
    {
        Task InsertAsync(HttpRequest request);
        AsyncTableQuery<T> Table<T>() where T : class;
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

        public AsyncTableQuery<T> Table<T>() where T : class
        {
            return sqlite.Table<T>();
        }
    }
}
