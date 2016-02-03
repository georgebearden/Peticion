using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Peticion.Tests
{
    public class AddingARequestToTheHistory
    {
        [Fact]
        public async void PersistsRequestIfItDoesNotExistAlready()
        {
            var sqlite = new Mock<ISQLiteAsyncConnection>();
            sqlite.Setup(s => s.Get<HttpRequest>()).Returns(Task.FromResult(new List<HttpRequest>()));

            var request = new HttpRequest {Method = HttpMethods.Get, Url = "http://localhost:3000/good"};
            var requestHistory = new RequestHistory(sqlite.Object);
            await requestHistory.AddRequestAsync(request);

            sqlite.Verify(s => s.InsertAsync(It.IsAny<HttpRequest>()), Times.Once());
        }

        [Fact]
        public async void DoesNotPersistRequestIfItAlreadyExists()
        {
            var request = new HttpRequest { Method = HttpMethods.Get, Url = "http://localhost:3000/good" };

            var sqlite = new Mock<ISQLiteAsyncConnection>();
            sqlite.Setup(s => s.Get<HttpRequest>()).Returns(Task.FromResult(new List<HttpRequest> { request }));

            var requestHistory = new RequestHistory(sqlite.Object);
            await requestHistory.AddRequestAsync(request);

            sqlite.Verify(s => s.InsertAsync(It.IsAny<HttpRequest>()), Times.Never());
        }
    }
}
