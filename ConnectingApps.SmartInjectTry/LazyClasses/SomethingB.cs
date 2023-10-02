namespace ConnectingApps.SmartInjectTry.LazyClasses
{
    public class SomethingB : ISomethingB
    {
        private readonly Lazy<ISomethingElseB> _somethingA;

        public SomethingB(Lazy<ISomethingElseB> somethingA)
        {
            _somethingA = somethingA;
        }
    }
}
