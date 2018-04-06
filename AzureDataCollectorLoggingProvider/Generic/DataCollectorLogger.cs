namespace AzureDataCollectorLoggingProvider.Generic
{
    /// <summary>
    ///  An <see cref="T:Microsoft.Extensions.Logging.ILogger" /> that writes logs to the Azure
    ///  Analytics HTTP Data Collector API with file system fail-back
    /// </summary>
    // ReSharper disable once ClassCanBeSealed.Global
    public class DataCollectorLogger : BatchLogger.BatchingLogger
    {
        #region Public Constructors

        public DataCollectorLogger(Microsoft.Extensions.Logging.ILoggerProvider loggerProvider, System.String categoryNameLogTypeName) : base(loggerProvider, categoryNameLogTypeName)
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
            this._provider = (DataCollectorLoggerProvider)loggerProvider;
            this._categoryNameLogType = categoryNameLogTypeName;
        }

        #endregion Public Constructors

        #region Private Fields

        private readonly System.String _categoryNameLogType;

        private readonly DataCollectorLoggerProvider _provider;

        #endregion Private Fields

        #region Public Properties

        // ReSharper disable once MemberCanBePrivate.Global
        [JetBrains.Annotations.UsedImplicitly]
        public DataCollectorLoggerProvider Provider { [JetBrains.Annotations.UsedImplicitly] get; }

        #endregion Public Properties

        #region Public Methods

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
        [JetBrains.Annotations.UsedImplicitly]
        public void Log<TState>(
            [JetBrains.Annotations.CanBeNull] Microsoft.Extensions.Logging.LogLevel? logLevel,
            Microsoft.Extensions.Logging.EventId eventId,
            [JetBrains.Annotations.NotNull] TState state,
            [JetBrains.Annotations.CanBeNull] System.Exception exception,
            [JetBrains.Annotations.NotNull] System.Func<TState, System.Exception, System.String> formatter,
            [JetBrains.Annotations.CanBeNull] System.String logType = null,
            [JetBrains.Annotations.CanBeNull] System.DateTimeOffset? timestamp = null,
            [JetBrains.Annotations.CanBeNull] System.Guid? correlationId = null)
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
            var builder =
                new LogMessage<TState>(logLevel, eventId, state, exception, formatter, this._categoryNameLogType, timestamp, correlationId);
            this._provider.AddMessage(builder);
        }

        #endregion Public Methods

        #region Private Methods

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
                new LogMessage<TState>(logLevel, eventId, state, exception,
                    formatter, this._categoryNameLogType);
            this._provider.AddMessage(builder);
        }

        #endregion Private Methods
    }
}
