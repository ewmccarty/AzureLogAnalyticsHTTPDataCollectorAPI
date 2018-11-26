namespace AzureDataCollectorLoggingProvider.Interfaces.BatchLogger
{
    public interface IBatchingLoggerOptions
    {
        #region Public Properties

        /// <summary>
        ///  Gets or sets the maximum size of the background log message queue or null for no limit.
        ///  After maximum queue size is reached log event sink would start blocking. Defaults to <c>null</c>.
        /// </summary>
        System.Int32? BackgroundQueueSize { get; set; }

        /// <summary>
        ///  Gets or sets a maximum number of events to include in a single batch or null for no limit.
        /// </summary>
        System.Int32? BatchSize { get; set; }

        /// <summary>
        ///  Gets or sets the period after which logs will be flushed to the store.
        /// </summary>
        System.TimeSpan FlushPeriod { get; set; }

        /// <summary>
        ///  Gets or sets value indicating if logger accepts and queues writes.
        /// </summary>
        System.Boolean IsEnabled { get; set; }

        #endregion Public Properties
    }
}
