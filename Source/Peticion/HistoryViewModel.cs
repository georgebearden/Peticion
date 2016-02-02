using System;
using System.Reactive.Linq;
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

            SelectedRequestObservable = this.ObservableForProperty(vm => vm.SelectedRequest).Select(r => r.Value);
        }

        HttpRequest selectedRequest;
        public HttpRequest SelectedRequest
        {
            get { return selectedRequest; }
            set { this.RaiseAndSetIfChanged(ref selectedRequest, value); }
        }
        public IObservable<HttpRequest> SelectedRequestObservable { get; private set; } 

        public ReactiveList<HttpRequest> Requests { get; private set; } 
    }
}
