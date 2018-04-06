namespace AzureDataCollectorLoggingProvider
{
    public static class Events
    {
        #region Internal Properties

        internal static Microsoft.Extensions.Logging.EventId E00650 { get; } = new Microsoft.Extensions.Logging.EventId(00650,
            "Error Calling PostAsync. Possible Internet Connectivity interruption.");

        internal static Microsoft.Extensions.Logging.EventId E00696 { get; } = new Microsoft.Extensions.Logging.EventId(00696,
            "Error Calling PostAsync. Possible Internet Connectivity interruption.");

        internal static Microsoft.Extensions.Logging.EventId E00793 { get; } = new Microsoft.Extensions.Logging.EventId(00793,
            "Write to the Azure App Services Data Collector fails, redirecting messages to the base FileLogger. Possible Internet Connectivity interruption.");

        #endregion Internal Properties
    }
}
