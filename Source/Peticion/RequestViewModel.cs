using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;

namespace Peticion
{
    public class RequestViewModel : ReactiveObject
    {
        readonly IRequestHistory requests;

        public RequestViewModel(IRequestHistory requests, IObservable<HttpRequest> selectedRequested )
        {
            this.requests = requests;
            HttpMethods = new ReactiveList<HttpMethods>(Enum.GetValues(typeof(HttpMethods)).Cast<HttpMethods>());
            SelectedHttpMethod = HttpMethods.First();

            var canSendRequest = this.WhenAny(x => x.Url, url => UrlHelper.IsValidUrl(url.Value));
            SendRequest = ReactiveCommand.CreateAsyncTask(canSendRequest, SendRequestImpl);

            this.ObservableForProperty(vm => vm.Url).Subscribe(_ =>
            {
                Reset();
            });

            selectedRequested.Subscribe(request =>
            {
                Reset();
                SelectedHttpMethod = request.Method;
                Url = request.Url;
                SendRequest.Execute(null);
            });
        }

        public void Reset()
        {
            ResponseStatusCode = string.Empty;
            ResponseBody = string.Empty;
        }

        public async Task SendRequestImpl(object _)
        {
            // No need to await this call.
            await requests.AddRequestAsync(new HttpRequest {Method = SelectedHttpMethod, Url = Url});

            var request = WebRequest.Create(Url);
            request.Method = SelectedHttpMethod.ToString();

            try
            {
                var response = await request.GetResponseAsync() as HttpWebResponse;
                if (response == null)
                    return;

                ResponseStatusCode = response.StatusCode.ToString();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    ResponseBody = await streamReader.ReadToEndAsync();
                }

                response.Close();
            }
            catch (WebException ex)
            {
                ResponseStatusCode = ex.Status.ToString();
                ResponseBody = ex.Message;
            }
        }

        string url;
        public string Url
        {
            get { return url; }
            set { this.RaiseAndSetIfChanged(ref url, value);}
        }

        HttpMethods selectedHttpMethod;
        public HttpMethods SelectedHttpMethod
        {
            get { return selectedHttpMethod; }
            set { this.RaiseAndSetIfChanged(ref selectedHttpMethod, value); }
        }

        string responseBody;

        public string ResponseBody
        {
            get { return responseBody; }
            set { this.RaiseAndSetIfChanged(ref responseBody, value); }
        }

        string responseStatusCode;

        public string ResponseStatusCode
        {
            get { return responseStatusCode; }
            set { this.RaiseAndSetIfChanged(ref responseStatusCode, value); }
        }

        public ReactiveCommand<Unit> SendRequest { get; private set; }
        public ReactiveList<HttpMethods>  HttpMethods { get; private set; }
    }
}
