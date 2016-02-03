using System;
using System.Threading.Tasks;
using System.Windows.Controls;
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
            var sqliteWrapper = new SQLiteAsyncConnectionImpl(sqlite);

            sqlite.CreateTableAsync<HttpRequest>().ContinueWith(_ =>
            {
                var requestHistory = new RequestHistory(sqliteWrapper);

                var historyViewModel = new HistoryViewModel(requestHistory);
                var historyView = new HistoryView(historyViewModel);
                Grid.SetColumn(historyView, 0);
                rootGrid.Children.Add(historyView);

                var requestViewModel = new RequestViewModel(requestHistory, historyViewModel.SelectedRequestObservable);
                var requestView = new RequestView(requestViewModel);
                Grid.SetColumn(requestView, 1);
                rootGrid.Children.Add(requestView);

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}