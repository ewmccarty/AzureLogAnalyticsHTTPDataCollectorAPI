namespace AzureDataCollectorLoggingProvider.Tests.Init
{
    internal class OptionsWrapper<T> : Microsoft.Extensions.Options.IOptions<T> where T : class, new()
    {
        #region Public Properties

        public T Value { get; set; }

        #endregion Public Properties

        #region Public Constructors

        public OptionsWrapper(T currentValue)
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
            this.Value = currentValue;
        }

        #endregion Public Constructors
    }
}
