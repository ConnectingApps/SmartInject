using ConnectingApps.SmartInjectTry.LazyClasses;
using FluentAssertions;

namespace ConnectingApps.SmartInjectTry.SelfhostTest
{
    public class LazyResolveTest
    {
        [Fact]
        public async Task ResolveSomethingA()
        {
            await VerifyResolve<ISomethingA>();
        }

        [Fact]
        public async Task ResolveSomething()
        {
            await VerifyResolve<ISomething>();
        }

        [Fact]
        public async Task ResolveSomethingB()
        {
            await VerifyResolve<ISomethingB>();
        }

        private async Task VerifyResolve<TToResolve>() where TToResolve : class
        {
            await using (var factory = new TestWebApplicationFactory<TToResolve>())
            {
                using (factory.CreateClient())
                {
                    factory.TypeToResolve.Value.Should().NotBeNull();
                }
            }
        }
    }
}
