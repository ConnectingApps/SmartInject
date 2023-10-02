using ConnectingApps.SmartInjectTry.LazyClasses;
using FluentAssertions;

namespace ConnectingApps.SmartInjectTry.SelfhostTest
{
    public class LazyResolveTest
    {
        [Fact]
        public async Task ResolveSomething()
        {
            await using (var factory = new TestWebApplicationFactory<ISomething>())
            {
                using (factory.CreateClient())
                {
                    factory.TypeToResolve.Value.Should().NotBeNull();
                }
            }
        }
    }
}
