namespace ConnectingApps.SmartInjectTry.LazyClasses
{
    public class SomethingElse : ISomethingElse
    {
        private readonly Lazy<ISomething> _something;

        public SomethingElse(Lazy<ISomething> something)
        {
            _something = something;
        }
    }
}
