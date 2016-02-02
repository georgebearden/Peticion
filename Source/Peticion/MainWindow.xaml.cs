using System;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.Win32;

namespace Peticion
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var connFactory = new Func<SQLiteConnectionWithLock>(() =>
                new SQLiteConnectionWithLock(
                    new SQLitePlatformWin32(),
                    new SQLiteConnectionString("requests.db", false)));

            var sqlite = new SQLiteAsyncConnection(connFactory);
            sqlite.CreateTableAsync<HttpRequest>();

            var requestHistory = new RequestHistory(sqlite);

            var viewModel = new RequestViewModel(requestHistory);
            var view = new RequestView(viewModel);
            Content = view;
        }
    }
}