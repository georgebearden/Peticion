using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Peticion.Tests
{
    public class AddingARequestToTheHistory
    {
        /// <summary>
        /// 4
        /// </summary>
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

        /// <summary>
        /// 5
        /// </summary>
        [Fact]
        public async void PersistsRequestIfMethodIsDifferentAndUrlIsTheSame()
        {
            var url = "http://localhost:3000/good";
            var oldRequest = new HttpRequest { Method = HttpMethods.Get, Url = url };

            var sqlite = new Mock<ISQLiteAsyncConnection>();
            sqlite.Setup(s => s.Get<HttpRequest>()).Returns(Task.FromResult(new List<HttpRequest> { oldRequest }));

            var requestHistory = new RequestHistory(sqlite.Object);
            await requestHistory.AddRequestAsync(new HttpRequest { Method = HttpMethods.Put, Url = url });

            sqlite.Verify(s => s.InsertAsync(It.IsAny<HttpRequest>()), Times.Once());
        }

        /// <summary>
        /// 6
        /// </summary>
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

        /// <summary>
        /// 7
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="times"></param>
        [Theory]
        [InlineData(HttpMethods.Get, "http://localhost:3000/good", 0)]
        [InlineData(HttpMethods.Put, "http://localhost:3000/good", 1)]
        [InlineData(HttpMethods.Get, "http://localhost:3000/bad", 1)]
        public async void DoesNotPersistRequestIfItAlreadyExists2(HttpMethods method, string url, int times)
        {
            var request = new HttpRequest { Method = HttpMethods.Get, Url = "http://localhost:3000/good" };

            var sqlite = new Mock<ISQLiteAsyncConnection>();
            sqlite.Setup(s => s.Get<HttpRequest>()).Returns(Task.FromResult(new List<HttpRequest> { request }));

            var newRequest = new HttpRequest {Method = method, Url = url};
            var requestHistory = new RequestHistory(sqlite.Object);
            await requestHistory.AddRequestAsync(newRequest);

            sqlite.Verify(s => s.InsertAsync(It.IsAny<HttpRequest>()), times.ToTimes());
        }
    }
}
