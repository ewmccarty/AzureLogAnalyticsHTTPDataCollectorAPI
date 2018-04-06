using System;

namespace AzureDataCollectorLoggingProvider.Generic
{
    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
    public struct LogMessage<TState> : Interfaces.Generic.ILogMessage<TState>
    {
        #region Public Constructors

        /// <inheritdoc cref="Microsoft.Extensions.Logging" />
        /// <summary>
        ///  LogMessage .ctor fashioned after Logger.Log Method Interface
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter"></param>
        /// <example>
        ///  <code>
        /// <![CDATA[
        /// Interfaces.Internal.Generic.ILogMessage<TState> builder = new Internal.Generic.LogMessage<TState>(logLevel, eventId, state, exception, formatter);
        /// ]]>
        ///  </code>
        /// </example>
        public LogMessage(Microsoft.Extensions.Logging.LogLevel logLevel, Microsoft.Extensions.Logging.EventId eventId,
            [JetBrains.Annotations.NotNull] TState state, Exception exception, Func<TState, Exception, String> formatter)
        {
            this.CorrelationId = Guid.NewGuid();
            this.EventId = eventId;
            this.Exception = exception;
            this.Formatter = formatter;
            this.LogLevel = logLevel;
            this.LogType = typeof(TState).FullName;
            this.State = state;
            this.Timestamp = DateTimeOffset.Now;
        }

        /// <summary>
        ///  LogMessage .ctor with all parameters
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">
        ///  string message formatter from the state and exception.
        ///  <code>
        /// in simplest form
        /// (state, ex) =&gt; state
        ///  </code>
        /// </param>
        /// <param name="logType"></param>
        /// <param name="timestamp"></param>
        /// <param name="correlationId"></param>
        /// <example>
        ///  <code>
        /// <![CDATA[
        /// Interfaces.Internal.Generic.ILogMessage<TState> builder = new Internal.Generic.LogMessage<TState>(logLevel, eventId, state, exception, formatter, logType, timestamp, correlationId);
        /// ]]>
        ///  </code>
        /// </example>
        public LogMessage(
            [JetBrains.Annotations.CanBeNull] Microsoft.Extensions.Logging.LogLevel? logLevel,
            Microsoft.Extensions.Logging.EventId eventId,
            [JetBrains.Annotations.NotNull] TState state,
            [JetBrains.Annotations.CanBeNull] Exception exception,
            [JetBrains.Annotations.CanBeNull] Func<TState, Exception, String> formatter,
            [JetBrains.Annotations.CanBeNull] String logType = "",
            [JetBrains.Annotations.CanBeNull] DateTimeOffset? timestamp = null,
            [JetBrains.Annotations.CanBeNull] Guid? correlationId = null)
        {
            this.CorrelationId = correlationId ?? Guid.NewGuid();
            this.EventId = eventId;
            this.Exception = exception;
            this.Formatter = formatter;
            this.LogLevel = logLevel ?? Microsoft.Extensions.Logging.LogLevel.Information;
            this.LogType = (logType == null || logType.Trim() == String.Empty) ? typeof(TState).FullName : logType;
            this.State = state;
            this.Timestamp = timestamp ?? DateTimeOffset.Now;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc />
        /// <summary>
        ///  Correlate a Unit of Work using a tracker value
        /// </summary>
        /// <remarks>
        ///  I have observed how Microsoft has use CorrelationId type values to group Units of Work,
        ///  and I think I'll incorporate that idea
        /// </remarks>
        [Newtonsoft.Json.JsonProperty]
        public Guid? CorrelationId { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///  Id of the event.
        /// </summary>
        /// <remarks>
        ///  <![CDATA[ The <see cref="Microsoft.Extensions.Logging"/> imposes an interface requiring
        ///  this parameter as part of the Log method the Microsoft.Extensions.Logging.EventId
        ///  eventId ]]>
        /// </remarks>
        [Newtonsoft.Json.JsonProperty]
        public Microsoft.Extensions.Logging.EventId? EventId { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///  The exception related to this entry.
        /// </summary>
        /// <remarks>
        ///  <![CDATA[ The <see cref="Microsoft.Extensions.Logging"/> imposes an interface requiring
        ///  this System.Exception exception parameter as part of the Log method ]]>
        /// </remarks>
        [Newtonsoft.Json.JsonProperty]
        public Exception Exception { get; set; }

        /// <summary>
        ///  Function to create a string message of the state and exception.
        /// </summary>
        /// <code>
        /// in simplest form
        /// (state, ex) =&gt; state
        /// </code>
        /// <remarks>
        ///  <![CDATA[ The <see cref="Microsoft.Extensions.Logging"/> imposes an interface requiring
        ///  this parameter as part of the Log() method Note that when JSON Serialized, this tends to
        ///  be a large object in comparision with other parts of the class, and so json
        ///  serialization is disabled by default. ]]>
        /// </remarks>
        /// <code>
        /// <![CDATA[
        /// public delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2)
        /// https://msdn.microsoft.com/en-us/library/bb534647(v=vs.110).aspx
        /// ]]>
        /// </code>
        // TODO Make the JSON Serialization of the class LogMessage property Formatted to be configurable
        [Newtonsoft.Json.JsonIgnore]
        public Func<TState, Exception, String> Formatter { get; set; }

        /// <inheritdoc />
        /// <inheritdoc cref="Microsoft.Extensions.Logging" />
        /// <summary>
        ///  Entry will be written on this level.
        /// </summary>
        /// <remarks>
        ///  <![CDATA[ The <see cref="Microsoft.Extensions.Logging"/> makes use of
        ///  Microsoft.Extensions.Logging.LogLevel logLevel for categorization of the Log entry, and
        ///  uses LogLevel to determine if this level of logging is enabled. ]]>
        /// </remarks>
        [Newtonsoft.Json.JsonProperty]
        public Microsoft.Extensions.Logging.LogLevel? LogLevel { get; set; }

        //System.Object Formatted { get; }
        /// <inheritdoc />
        /// <inheritdoc cref="Microsoft.Extensions.Logging" />
        /// <summary>
        ///  Log-Type is name of the event type that is being submitted to Log Analytics. It is the
        ///  The categoryname for messages produced by the logger.
        /// </summary>
        /// <remarks>
        ///  <![CDATA[ The Log Analytics HTTP Data Collector API HTTPMessage Request Header impose an
        ///  interface requiring the "Log-Type" (LogType) parameter. Similarly the <see
        ///  cref="Microsoft.Extensions.Logging"/> makes use of TState state for typeing the
        ///  Logger<TState> in the .ctor and method Provider.CreateLogger(System.String
        ///  categoryName). ]]>
        /// </remarks>
        [Newtonsoft.Json.JsonProperty]
        public String LogType { get; set; }

        /// <inheritdoc cref="Microsoft.Extensions.Logging.AzureAppServices.Internal" />
        /// <summary>
        ///  Intended somewhat for backward compatiblity with Microsoft.Extensions.Logging.AzureAppServices.Internal.LogMessage
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public String Message => this.ToJson();

        /// <inheritdoc />
        /// <summary>
        ///  (TState)State JSON Serializable Object or JSON object with key/value Pairs, or System.String
        /// </summary>
        /// <remarks>
        ///  <![CDATA[ The ILogger State object and the Log Analytics HTTP Data Collector API
        ///  HTTPMessage Content both imposes an interface requiring this parameter in the Method
        ///  Log(), and BatchStart() ]]>
        /// </remarks>
        [Newtonsoft.Json.JsonProperty]
        public TState State { get; set; }

        /// <inheritdoc />
        /// <inheritdoc cref="Microsoft.Extensions.Logging.AzureAppServices.Internal" />
        /// <summary>
        ///  time-generated-field The name of a field in the data that contains the timestamp of the
        ///  data item. If you specify a field then its contents are used for TimeGenerated. If this
        ///  field isn’t specified, the default for TimeGenerated is the time that the message is
        ///  ingested. The contents of the message field should follow the ISO 8601 format YYYY-MM-DDThh:mm:ssZ.
        ///
        ///  Also Intended somewhat for backward compatiblity with Microsoft.Extensions.Logging.AzureAppServices.Internal.LogMessage
        /// </summary>
        /// <summary>
        /// </summary>
        /// <remarks>
        ///  <![CDATA[ The Log Analytics HTTP Data Collector API HTTPMessage Request Header impose an
        ///  interface optionally requiring teh time-generated-field parameter, or the default
        ///  inception time ]]>
        /// </remarks>
        /// <remarks>
        ///  <![CDATA[
        ///  - See Log Analytics HTTP Data Collector API (https://docs.microsoft.com/en-us/azure/log-analytics/log-analytics-data-collector-api)
        ///  - Request headers
        ///  - time-generated-field : The name of a field in the data that contains the timestamp of
        ///    the data item. If you specify a field then its contents are used for TimeGenerated. If
        ///    this field isn’t specified, the default for TimeGenerated is the time that the message
        ///    is ingested. The contents of the message field should follow the ISO 8601 format
        ///    YYYY-MM-DDThh:mm:ssZ. ]]>
        /// </remarks>
        /// <remarks>
        ///  <![CDATA[ https://www.newtonsoft.com/json/help/html/DatesInJSON.htm IsoDateTimeConverter
        ///  From Json.NET 4.5 and onwards dates are written using the ISO 8601 format by default,
        ///  and using this converter is unnecessary. IsoDateTimeConverter serializes a DateTime to
        ///  an ISO 8601 formatter string: "2009-02-15T00:00:00Z" The IsoDateTimeConverter class has
        ///  a property, DateTimeFormat, to further customize the formatter string. ]]>
        /// </remarks>
        [Newtonsoft.Json.JsonProperty]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        ///  <![CDATA[ Converts from LogMessage<TState> to LogMessage<Object> ]]>
        /// </summary>
        /// <remarks>
        ///  <![CDATA[ Because any TState can be converted to a <xref:System.Object>, there's no need
        ///  to force users to be explicit about the conversion. ]]>
        /// </remarks>
        /// <param name="v"></param>
        public static implicit operator LogMessage<Object>(LogMessage<TState> v)
        {
            var logMessage = new LogMessage<Object>
            {
                CorrelationId = v.CorrelationId,
                EventId = v.EventId,
                Exception = v.Exception,
                Formatter = v.Formatter as Func<Object, Exception, String>,
                LogLevel = v.LogLevel,
                LogType = v.LogType,
                State = v.State,
                Timestamp = v.Timestamp
            };

            return logMessage;
        }

        /// <inheritdoc />
        /// <summary>
        ///  Returns a json formatter string that represents the current object.
        /// </summary>
        /// <returns>A json formatter string that represents the current object.</returns>
        [JetBrains.Annotations.UsedImplicitly]
        public String ToJson() =>
            Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented,
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None,
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                });

        #endregion Public Methods

        //public delegate String ToJson(Object obj, Exception ex);
    }
}
