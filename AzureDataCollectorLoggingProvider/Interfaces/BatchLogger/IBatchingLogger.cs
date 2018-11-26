namespace AzureDataCollectorLoggingProvider.Interfaces.BatchLogger
{
    public interface IBatchingLogger : Microsoft.Extensions.Logging.ILogger
    {
        #region Public Methods

        /// <inheritdoc cref="Microsoft.Extensions.Logging.ILogger" />
        /// <summary>
        ///  Begins a logical operation scope.
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state">The identifier for the scope.</param>
        /// <returns>An IDisposable that ends the logical operation scope on dispose.</returns>
        [JetBrains.Annotations.UsedImplicitly]
        new System.IDisposable BeginScope<TState>(TState state);

        /// <inheritdoc cref="Microsoft.Extensions.Logging.ILogger" />
        /// <summary>
        ///  Checks if the given logLevel is enabled.
        /// </summary>
        /// <param name="logLevel">level to be checked.</param>
        /// <returns>true if enabled.</returns>
        [JetBrains.Annotations.UsedImplicitly]
        new System.Boolean IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel);

        /// <summary>
        ///  Writes a log entry.
        /// </summary>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">
        ///  Function to create a <c>string</c> message of the <paramref name="state" /> and <paramref name="exception" />.
        /// </param>
        [JetBrains.Annotations.UsedImplicitly]
        new void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, Microsoft.Extensions.Logging.EventId eventId, TState state, System.Exception exception, System.Func<TState, System.Exception, System.String> formatter);

        /// <summary>
        ///  Writes a log entry.
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">
        ///  Function to create a <c>string</c> message of the <paramref name="state" /> and <paramref name="exception" />.
        /// </param>
        [JetBrains.Annotations.UsedImplicitly]
        void Log<TState>(System.DateTimeOffset timestamp, Microsoft.Extensions.Logging.LogLevel logLevel, Microsoft.Extensions.Logging.EventId eventId, TState state, System.Exception exception, System.Func<TState, System.Exception, System.String> formatter);

        /// <summary>
        ///  Writes a log entry.
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">
        ///  Function to create a <c>string</c> message of the <paramref name="state" /> and <paramref name="exception" />.
        /// </param>
        /// <param name="correlationId"></param>
        [JetBrains.Annotations.UsedImplicitly]
        void Log<TState>(System.DateTimeOffset timestamp, Microsoft.Extensions.Logging.LogLevel logLevel, Microsoft.Extensions.Logging.EventId eventId, TState state, System.Exception exception, System.Func<TState, System.Exception, System.String> formatter, System.Guid correlationId);

        #endregion Public Methods
    }
}
