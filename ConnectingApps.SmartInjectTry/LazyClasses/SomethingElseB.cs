namespace ConnectingApps.SmartInjectTry.LazyClasses
{
    public class SomethingElseB : ISomethingElseB
    {
        private readonly Lazy<ISomethingB> _something;

        public SomethingElseB(Lazy<ISomethingB> something)
        {
            _something = something;
        }
    }
}
