namespace AzureDataCollectorLoggingProvider.BatchLogger
{
    // ReSharper disable once MemberCanBeInternal

    /// <inheritdoc />
    // ReSharper disable once ClassCanBeSealed.Global
    public class BatchingLogger : Interfaces.BatchLogger.IBatchingLogger
    {
        #region Public Constructors

        // ReSharper disable once MemberCanBeInternal
        public BatchingLogger(Microsoft.Extensions.Logging.ILoggerProvider loggerProvider, System.String categoryNameLogTypeName)
        {
            this._provider = (BatchingLoggerProvider)loggerProvider;
            this._categoryNameLogType = categoryNameLogTypeName;
        }

        #endregion Public Constructors

        #region Private Fields

        private readonly System.String _categoryNameLogType;
        private readonly BatchingLoggerProvider _provider;

        #endregion Private Fields

        #region Public Methods

        #region Implementation of ILogger

        /// <inheritdoc cref="Microsoft.Extensions.Logging.ILogger" />
        /// ///
        /// <summary>
        ///  Begins a logical operation scope. ///
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// ///
        /// <param name="state">The identifier for the scope.</param>
        /// <returns>An IDisposable that ends the logical operation scope on dispose.</returns>
        [JetBrains.Annotations.CanBeNull]
        public System.IDisposable BeginScope<TState>(TState state) => null;

        /// <inheritdoc cref="Microsoft.Extensions.Logging.ILogger" />
        /// <summary>
        ///  Checks if the given logLevel is enabled.
        /// </summary>
        /// ///
        /// <param name="logLevel">level to be checked.</param>
        /// <returns>true if enabled.</returns>
        public System.Boolean IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel) => logLevel != Microsoft.Extensions.Logging.LogLevel.None;

        /// <summary>
        ///  Writes a log entry.
        /// </summary>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">
        ///  Function to create a <c>string</c> message of the /// <paramref name="state" /> and /// <paramref name="exception" />.
        /// </param>
        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, Microsoft.Extensions.Logging.EventId eventId, TState state, System.Exception exception, System.Func<TState, System.Exception, System.String> formatter)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }
            // new Internal.Generic.LogMessage<TState>(logLevel, eventId, state, exception,
            // formatted, logType, timestamp, correlationId);
            var builder = new Generic.LogMessage<TState>(logLevel, eventId, state, exception,
                formatter, this._categoryNameLogType);
            this._provider.AddMessage(builder);
        }

        /// <summary>
        ///  Writes a log entry.
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">
        ///  Function to create a <c>string</c> message of the /// <paramref name="state" /> and /// <paramref name="exception" />.
        /// </param>
        public void Log<TState>(System.DateTimeOffset timestamp, Microsoft.Extensions.Logging.LogLevel logLevel, Microsoft.Extensions.Logging.EventId eventId, TState state, System.Exception exception, System.Func<TState, System.Exception, System.String> formatter)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }
            // new Internal.Generic.LogMessage<TState>(logLevel, eventId, state, exception,
            // formatted, logType, timestamp, correlationId);
            var builder = new Generic.LogMessage<TState>(logLevel: logLevel, eventId: eventId, state: state, exception: exception,
                formatter: formatter, logType: this._categoryNameLogType, timestamp: timestamp);
            this._provider.AddMessage(builder);
        }

        /// <summary>
        ///  Writes a log entry.
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">
        ///  Function to create a <c>string</c> message of the /// <paramref name="state" /> and /// <paramref name="exception" />.
        /// </param>
        /// <param name="correlationId"></param>
        public void Log<TState>(System.DateTimeOffset timestamp, Microsoft.Extensions.Logging.LogLevel logLevel, Microsoft.Extensions.Logging.EventId eventId, TState state, System.Exception exception, System.Func<TState, System.Exception, System.String> formatter, System.Guid correlationId)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }
            // new Internal.Generic.LogMessage<TState>(logLevel, eventId, state, exception,
            // formatted, logType, timestamp, correlationId);
            var builder = new Generic.LogMessage<TState>(logLevel: logLevel, eventId: eventId, state: state, exception: exception,
                formatter: formatter, logType: this._categoryNameLogType, timestamp: timestamp, correlationId: correlationId);
            this._provider.AddMessage(builder);
        }

        #endregion Implementation of ILogger

        #endregion Public Methods
    }
}
