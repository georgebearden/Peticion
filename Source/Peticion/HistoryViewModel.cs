using ReactiveUI;

namespace Peticion
{
    public class HistoryViewModel : ReactiveObject
    {
        public HistoryViewModel(IRequestHistory requests)
        {
            requests.GetRequestsAsync().ContinueWith(continuation =>
            {
                Requests = new ReactiveList<HttpRequest>(continuation.Result);
            });
        }

        public ReactiveList<HttpRequest> Requests { get; private set; } 
    }
}
