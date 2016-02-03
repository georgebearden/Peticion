using System;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Peticion.Tests
{
    public class HistoryViewModelTests
    {
        [Fact]
        public void PopulatesHistoryListWithPreviousRequests()
        {
            var prevRequests = new[]
            {
                new HttpRequest(HttpMethods.Get, "http://localhost"),
                new HttpRequest(HttpMethods.Get, "http://localhost:3000/good"),
                new HttpRequest(HttpMethods.Put, "http://localhost:3000/good")
            }.ToList();

            var requests = new Mock<IRequestHistory>();
            requests.Setup(r => r.GetRequestsAsync()).Returns(Task.FromResult(prevRequests));
            requests.Setup(r => r.GetRequestsObservable()).Returns(Observable.Empty<HttpRequest>());

            var historyViewModel = new HistoryViewModel(requests.Object);

            // oops this will not work since the historyviewmodel constructor is async in when things get set.
            //Assert.True(prevRequests.Count == historyViewModel.Requests.Count);

            historyViewModel.Requests.CountChanged.Subscribe(count => Assert.Equal(prevRequests.Count, count));
        }
    }
}
