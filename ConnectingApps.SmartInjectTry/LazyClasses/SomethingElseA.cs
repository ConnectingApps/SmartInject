namespace ConnectingApps.SmartInjectTry.LazyClasses
{
    public class SomethingElseA : ISomethingElseA
    {
        private readonly Lazy<ISomethingA> _somethingA;

        public SomethingElseA(Lazy<ISomethingA> somethingA)
        {
            _somethingA = somethingA;
        }
    }
}
