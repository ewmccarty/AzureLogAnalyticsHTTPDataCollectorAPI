namespace AzureDataCollectorLoggingProvider.Interfaces.Generic
{
    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
    public interface ILogMessage<TState>
    {
        #region Public Methods

        /// <summary>
        ///  Returns a json formatted string that represents the current object.
        /// </summary>
        /// <remarks>Not serialized because mode is opt-in///</remarks>
        /// <returns>A json formatted string that represents the current object.///</returns>
        [JetBrains.Annotations.UsedImplicitly]
        System.String ToJson();

        #endregion Public Methods

        #region Public Properties

        /// <summary>
        ///  Correlate a Unit of Work using a tracker value
        /// </summary>
        /// <remarks>
        ///  I have observed how Microsoft has use CorrelationId type values to group Units of Work,
        ///  and I think I'll incorporate that idea
        /// </remarks>
        [JetBrains.Annotations.CanBeNull]
        [Newtonsoft.Json.JsonProperty]
        System.Guid? CorrelationId { get; }

        /// <summary>
        ///  Id of the event.
        /// </summary>
        /// <remarks>
        ///  <![CDATA[ The /// <see cref="Microsoft.Extensions.Logging"/> imposes an interface
        ///  requiring this parameter as part of the Log method the
        ///  Microsoft.Extensions.Logging.EventId eventId ]]>
        /// </remarks>
        [JetBrains.Annotations.CanBeNull]
        [Newtonsoft.Json.JsonProperty]
        Microsoft.Extensions.Logging.EventId? EventId { get; }

        /// <summary>
        ///  The exception related to this entry.
        /// </summary>
        /// <remarks>
        ///  <![CDATA[ The /// <see cref="Microsoft.Extensions.Logging"/> imposes an interface
        ///  requiring this System.Exception exception parameter as part of the Log method ]]>
        /// </remarks>
        [JetBrains.Annotations.CanBeNull]
        [Newtonsoft.Json.JsonProperty]
        System.Exception Exception { get; }

        /// <summary> Function to create a string message of the state and exception. </summary> <code>
        // in simplest form
        /// (state, ex) =&gt; state </code> <remarks> <![CDATA[ The <see
        /// cref="Microsoft.Extensions.Logging"/> imposes an interface requiring this parameter as
        /// part of the Log() method Note that when JSON Serialized, this tends to be a large object
        /// in comparision with other parts of the class, and so json serialization is disabled by
        /// default. ]]> </remarks> <code> <![CDATA[ public delegate TResult Func/// <in T1, in T2,
        /// out TResult>(T1 arg1, T2 arg2)
        /// https://msdn.microsoft.com/en-us/library/bb534647(v=vs.110).aspx ]]> </code>
        // TODO Make the JSON Serialization of the class LogMessage property Formatted to be configurable
        [Newtonsoft.Json.JsonIgnore]
        [JetBrains.Annotations.CanBeNull]
        System.Func<TState, System.Exception, System.String> Formatter { get; }

        /// <inheritdoc cref="Microsoft.Extensions.Logging" />
        /// <summary>
        ///  Entry will be written on this level.
        /// </summary>
        /// <remarks>
        ///  <![CDATA[ The /// <see cref="Microsoft.Extensions.Logging"/> makes use of
        ///  Microsoft.Extensions.Logging.LogLevel logLevel for categorization of the Log entry, and
        ///  uses LogLevel to determine if this level of logging is enabled. ]]>
        /// </remarks>
        [JetBrains.Annotations.CanBeNull]
        [Newtonsoft.Json.JsonProperty]
        Microsoft.Extensions.Logging.LogLevel? LogLevel { get; }

        /// <inheritdoc cref="Microsoft.Extensions.Logging" />
        /// <summary>
        ///  Log-Type is name of the event type that is being submitted to Log Analytics. It is the
        ///  The category name for messages produced by the logger.
        /// </summary>
        /// <remarks>
        ///  <![CDATA[ The Log Analytics HTTP Data Collector API HTTPMessage Request Header impose an
        ///  interface requiring the "Log-Type" (LogType) parameter. Similarly the <see
        ///  cref="Microsoft.Extensions.Logging"/> makes use of TState state for typeing the Logger
        ///  <TState> in the .ctor and method Provider.CreateLogger(System.String categoryName). ]]>
        /// </remarks>
        [JetBrains.Annotations.NotNull]
        [Newtonsoft.Json.JsonProperty]
        System.String LogType { get; }

        /// <inheritdoc cref="Microsoft.Extensions.Logging.AzureAppServices.Internal" />
        /// <summary>
        ///  Intended somewhat for backward compatiblity with Microsoft.Extensions.Logging.AzureAppServices.Internal.LogMessage
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        System.String Message { get; }

        /// <summary>
        ///  (TState)State JSON Serializable Object or JSON object with key/value Pairs, or System.String
        /// </summary>
        /// <remarks>
        ///  <![CDATA[ The ILogger State object and the Log Analytics HTTP Data Collector API
        ///  HTTPMessage Content both imposes an interface requiring this parameter in the Method
        ///  Log(), and BatchStart() ]]>
        /// </remarks>
        [JetBrains.Annotations.NotNull]
        [Newtonsoft.Json.JsonProperty]
        TState State { get; }

        /// <inheritdoc cref="Microsoft.Extensions.Logging.AzureAppServices.Internal" />
        /// <summary>
        ///  time-generated-field The name of a field in the data that contains the timestamp of the
        ///  data item. If you specify a field then its contents are used for TimeGenerated. If this
        ///  field isn’t specified, the default for TimeGenerated is the time that the message is
        ///  ingested. The contents of the message field should follow the ISO 8601 format YYYY-MM-DDThh:mm:ssZ.
        ///
        ///  Also Intended somewhat for backward compatiblity with Microsoft.Extensions.Logging.AzureAppServices.Internal.LogMessage
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
        ///  an ISO 8601 formatted string: "2009-02-15T00:00:00Z" The IsoDateTimeConverter class has
        ///  a property, DateTimeFormat, to further customize the formatted string. ]]>
        /// </remarks>
        [Newtonsoft.Json.JsonProperty]
        System.DateTimeOffset Timestamp { get; }

        #endregion Public Properties
    }
}
