/*
 * Derived from https://docs.microsoft.com/en-us/azure/log-analytics/log-analytics-data-collector-api
 * Derived from NetEscapades.Extensions.Logging.RollingFile
 * Erik w. McCarty 2018-03026
 */

namespace AzureDataCollectorLoggingProvider
{
    /// <summary>
    ///  An <see cref="T:Microsoft.Extensions.Logging.ILogger" /> that writes logs to the Azure
    ///  Analytics HTTP Data Collector API with file system fail-back
    /// </summary>
    public sealed class DataCollectorLogger : BatchLogger.BatchingLogger
    {
        #region Public Constructors

        // ReSharper disable once MemberCanBeInternal
        public DataCollectorLogger(DataCollectorLoggerProvider loggerProvider, System.String categoryNameLogTypeName) :
            base(loggerProvider, categoryNameLogTypeName)
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
            this._provider = loggerProvider;
            this._categoryNameLogType = categoryNameLogTypeName;
        }

        #endregion Public Constructors

        #region Public Properties

        // ReSharper disable once MemberCanBePrivate.Global
        [JetBrains.Annotations.UsedImplicitly]
        public DataCollectorLoggerProvider Provider { [JetBrains.Annotations.UsedImplicitly] get; }

        #endregion Public Properties

        #region Private Fields

        private readonly System.String _categoryNameLogType;
        private readonly DataCollectorLoggerProvider _provider;

        #endregion Private Fields

        #region Implementation of ILogger

        /// <summary>
        ///  Writes a log entry
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logType"></param>
        /// <param name="timestamp"></param>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        /// <param name="correlationId"></param>
        public void Log<TState>(
            [JetBrains.Annotations.CanBeNull] Microsoft.Extensions.Logging.LogLevel? logLevel,
            Microsoft.Extensions.Logging.EventId eventId,
            [JetBrains.Annotations.NotNull] TState state,
            [JetBrains.Annotations.CanBeNull] System.Exception exception,
            [JetBrains.Annotations.NotNull] System.Func<TState, System.Exception, System.String> formatter,
            // ReSharper disable once UnusedParameter.Global
            [JetBrains.Annotations.CanBeNull] System.String logType = null,
            [JetBrains.Annotations.CanBeNull] System.DateTimeOffset? timestamp = null,
            [JetBrains.Annotations.CanBeNull] System.Guid? correlationId = null)
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
            var builder =
                new Generic.LogMessage<TState>(logLevel, eventId, state, exception, formatter, this._categoryNameLogType, timestamp, correlationId);
            this._provider.AddMessage(builder);
        }

        /// <inheritdoc cref="Microsoft.Extensions.Logging.ILogger" />
        /// <summary>
        ///  Writes a log entry.
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">
        ///  Function to create a string message of the state and exception.
        ///  <code>
        /// in simplest form
        /// (state, ex) =&gt; state
        ///  </code>
        /// </param>
        [JetBrains.Annotations.UsedImplicitly]
        private new void Log<TState>(
            Microsoft.Extensions.Logging.LogLevel logLevel,
            Microsoft.Extensions.Logging.EventId eventId,
            [JetBrains.Annotations.NotNull] TState state,
            System.Exception exception,
            [JetBrains.Annotations.NotNull] System.Func<TState, System.Exception, System.String> formatter)
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif

            var builder =
                new Generic.LogMessage<TState>(logLevel, eventId, state, exception,
                    formatter, this._categoryNameLogType);
            this._provider.AddMessage(builder);
        }

        #endregion Implementation of ILogger
    }
}
