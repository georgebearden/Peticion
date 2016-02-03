using Moq;

namespace Peticion.Tests
{
    public static class TimesHelper
    {
        public static Times ToTimes(this int i)
        {
            return Times.Exactly(i);
        }
    }
}
