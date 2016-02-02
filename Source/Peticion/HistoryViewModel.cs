using System;
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

            requests.GetRequests().Subscribe(request =>
            {
                Requests.Add(request);
            });
        }

        public ReactiveList<HttpRequest> Requests { get; private set; } 
    }
}
