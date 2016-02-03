using Moq;
using Xunit;

namespace Peticion.Tests
{
    public class AddingARequestToTheHistory
    {
        [Fact]
        public void InsertsNewRequestIfItDoesNotExistAlready()
        {
            var sqlite = new Mock<ISQLiteAsyncConnection>();
            var requestHistory = new RequestHistory(sqlite.Object);
        }
    }
}
