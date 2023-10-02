namespace ConnectingApps.SmartInjectTry.LazyClasses
{
    public class SomethingA : ISomethingA
    {
        private readonly Lazy<ISomethingElseA> _somethingB;

        public SomethingA(Lazy<ISomethingElseA> somethingB)
        {
            _somethingB = somethingB;
        }
    }
}
