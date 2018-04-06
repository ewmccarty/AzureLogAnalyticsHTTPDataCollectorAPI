/*
 * Derived from https://docs.microsoft.com/en-us/azure/log-analytics/log-analytics-data-collector-api
 * Derived from NetEscapades.Extensions.Logging.RollingFile
 * Erik w. McCarty 2018-03026
 */
// ReSharper disable MemberCanBePrivate.Global

namespace AzureDataCollectorLoggingProvider
{
    // <inheritdoc />
    // <summary>
    //Returns a json formatter string that represents the current object.
    // </summary>
    // <returns>A json formatter string that represents the current object.</returns>
    //[JetBrains.Annotations.UsedImplicitly]
    //public delegate System.String ToJson();

    /// <summary>
    ///  Extensions for adding the to theILoggerProvider that writes logs to the Azure Analytics HTTP
    ///  Data Collector API with file system fail-back
    /// </summary>
    /// <remarks><see><cref>Microsoft.Extensions.Logging.AzureAppServicesProvider</cref><cref>Microsoft.Extensions.Logging.LoggingBuilder</cref><cref>global::Microsoft.Extensions.Logging.ILoggerProvider</cref></see></remarks>
    public static class DataCollectorProviderExtensions
    {
        #region Internal Methods

        /// <summary>
        ///  Adds a file logger named 'File' to the factory.
        /// </summary>
        /// <param name="builder">The see Microsoft.Extensions.Logging.ILoggingBuilder to use.</param>
        [JetBrains.Annotations.NotNull]
        // ReSharper disable once UnusedMethodReturnValue.Global
        public static Microsoft.Extensions.Logging.ILoggingBuilder AddAzureDataCollector(
            [JetBrains.Annotations.NotNull] this Microsoft.Extensions.Logging.ILoggingBuilder builder)
        {
            Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions
                .AddSingleton<Microsoft.Extensions.Logging.ILoggerProvider, DataCollectorLoggerProvider>(
                    builder.Services);
            return builder;
        }

        /// <summary>
        ///  Adds a file logger named 'File' to the factory.
        /// </summary>
        /// <param name="builder">The see Microsoft.Extensions.Logging.ILoggingBuilder to use.</param>
        /// <param name="filename">Sets the filename prefix to use for log files</param>
        [JetBrains.Annotations.NotNull]
        [JetBrains.Annotations.UsedImplicitly]
        public static Microsoft.Extensions.Logging.ILoggingBuilder AddAzureDataCollector(
            [JetBrains.Annotations.NotNull] this Microsoft.Extensions.Logging.ILoggingBuilder builder, System.String filename)
        {
            //builder.AddAzureDataCollector(options => options.FileName = "log-");
            builder.AddAzureDataCollector();
            return builder;
        }

        /// <summary>
        ///  Adds a file logger named 'File' to the factory.
        /// </summary>
        /// <param name="builder">The see Microsoft.Extensions.Logging.ILoggingBuilder to use.</param>
        /// <param name="configure">
        ///  Configure an instance of the <see cref="RollingFileLogger.FileLoggerOptions" /> to set
        ///  logging options
        /// </param>
        [JetBrains.Annotations.NotNull]
        [JetBrains.Annotations.UsedImplicitly]
        public static Microsoft.Extensions.Logging.ILoggingBuilder AddAzureDataCollector(
            [JetBrains.Annotations.NotNull] this Microsoft.Extensions.Logging.ILoggingBuilder builder,
            //[JetBrains.Annotations.NotNull] System.Action<FileLogger.FileLoggerOptions> configure
            [JetBrains.Annotations.NotNull] System.Action<BatchLogger.BatchingLogger> configure
            )
        {
            if (configure == null)
            {
                throw new System.ArgumentNullException(nameof(configure));
            }

            builder.AddAzureDataCollector();
            Microsoft.Extensions.DependencyInjection.OptionsServiceCollectionExtensions.Configure(
                builder.Services, configure);

            return builder;
        }

        #endregion Internal Methods
    }
}
