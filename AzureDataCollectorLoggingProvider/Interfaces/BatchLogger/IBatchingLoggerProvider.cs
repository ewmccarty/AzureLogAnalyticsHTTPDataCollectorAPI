namespace AzureDataCollectorLoggingProvider.Interfaces.BatchLogger
{
    public interface IBatchingLoggerProvider : Microsoft.Extensions.Logging.ILoggerProvider
    {
        #region Public Methods

        new Microsoft.Extensions.Logging.ILogger CreateLogger(System.String categoryName);

        #endregion Public Methods
    }
}
