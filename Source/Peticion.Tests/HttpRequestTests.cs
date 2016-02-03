using Xunit;

namespace Peticion.Tests
{
    public class HttpRequestTests
    {
        public class TheEqualsMethod
        {
            /// <summary>
            /// 1
            /// </summary>
            [Fact]
            public void ReturnsTrueWhenTheMethodAndUrlAreTheSame()
            {
                var method = HttpMethods.Get;
                var url = "http://localhost:3000/";

                var request1 = new HttpRequest(method, url);
                var request2 = new HttpRequest(method, url);

                Assert.True(request1.Equals(request2));
            }

            /// <summary>
            /// 2
            /// </summary>
            [Fact]
            public void ReturnsFalseWhenTheMethodIsDifferent()
            {
                var url = "http://localhost:3000/";

                var request1 = new HttpRequest(HttpMethods.Get, url);
                var request2 = new HttpRequest(HttpMethods.Delete, url);

                Assert.False(request1.Equals(request2));
            }

            /// <summary>
            /// 3
            /// </summary>
            [Fact]
            public void ReturnsFalseWhenTheUrlIsDifferent()
            {
                var method = HttpMethods.Get;

                var request1 = new HttpRequest(method, "http://localhost:3000/good");
                var request2 = new HttpRequest(method, "http://localhost:3000/bad");

                Assert.False(request1.Equals(request2));
            }

            /// <summary>
            /// 8
            /// </summary>
            /// <param name="method1"></param>
            /// <param name="url1"></param>
            /// <param name="method2"></param>
            /// <param name="url2"></param>
            /// <param name="expectedResult"></param>
            [Theory]
            [InlineData(HttpMethods.Get, "http://localhost:3000/good", HttpMethods.Get, "http://localhost:3000/good", true)]
            [InlineData(HttpMethods.Get, "http://localhost:3000/good", HttpMethods.Delete, "http://localhost:3000/good", false)]
            [InlineData(HttpMethods.Get, "http://localhost:3000/good", HttpMethods.Get, "http://localhost:3000/bad", false)]
            public void BetterReturnsTrueWhenTheMethodAndUrlAreTheSame(HttpMethods method1, string url1, 
                HttpMethods method2, string url2, bool expectedResult)
            {
                var request1 = new HttpRequest(method1, url1);
                var request2 = new HttpRequest(method2, url2);

                Assert.True(request1.Equals(request2) == expectedResult);
            }
        }
    }
}
