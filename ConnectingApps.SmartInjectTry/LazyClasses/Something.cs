namespace ConnectingApps.SmartInjectTry.LazyClasses
{
    public class Something : ISomething
    {
        private readonly Lazy<ISomethingElse> _somethingElse;

        public Something(Lazy<ISomethingElse> somethingElse)
        {
            _somethingElse = somethingElse;
        }
    }
}
