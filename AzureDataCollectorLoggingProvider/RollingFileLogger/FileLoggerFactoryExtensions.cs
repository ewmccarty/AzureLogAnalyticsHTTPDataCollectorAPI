using OptionsServiceCollectionExtensions = Microsoft.Extensions.DependencyInjection.OptionsServiceCollectionExtensions;
using ServiceCollectionServiceExtensions = Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions;

// ReSharper disable MemberCanBePrivate.Global ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable UnusedMember.Global ReSharper disable UnusedParameter.Global

namespace AzureDataCollectorLoggingProvider.RollingFileLogger
{
    /// <summary>
    ///  Extensions for adding the <see cref="FileLoggerProvider" /> to the <see cref="Microsoft.Extensions.Logging.ILoggingBuilder" />
    /// </summary>
    [JetBrains.Annotations.UsedImplicitly]
    public static class FileLoggerFactoryExtensions
    {
        #region Public Methods

        /// <summary>
        ///  Adds a file logger named 'File' to the factory.
        /// </summary>
        /// <param name="builder">
        ///  The <see cref="Microsoft.Extensions.Logging.ILoggingBuilder" /> to use.
        /// </param>
        [JetBrains.Annotations.NotNull]
        // ReSharper disable once UnusedMethodReturnValue.Global
        public static Microsoft.Extensions.Logging.ILoggingBuilder AddFile(
            [JetBrains.Annotations.NotNull] this Microsoft.Extensions.Logging.ILoggingBuilder builder)
        {
            ServiceCollectionServiceExtensions
                .AddSingleton<Microsoft.Extensions.Logging.ILoggerProvider, FileLoggerProvider>(builder.Services);
            return builder;
        }

        /// <summary>
        ///  Adds a file logger named 'File' to the factory.
        /// </summary>
        /// <param name="builder">
        ///  The <see cref="Microsoft.Extensions.Logging.ILoggingBuilder" /> to use.
        /// </param>
        /// <param name="filename">Sets the filename prefix to use for log files</param>
        [JetBrains.Annotations.NotNull]
        public static Microsoft.Extensions.Logging.ILoggingBuilder AddFile(
            // ReSharper disable once UnusedParameter.Global
            [JetBrains.Annotations.NotNull] this Microsoft.Extensions.Logging.ILoggingBuilder builder, [JetBrains.Annotations.NotNull] System.String filename)
        {
            builder.AddFile(options => options.FileName = "log-");
            return builder;
        }

        /// <summary>
        ///  Adds a file logger named 'File' to the factory.
        /// </summary>
        /// <param name="builder">
        ///  The <see cref="Microsoft.Extensions.Logging.ILoggingBuilder" /> to use.
        /// </param>
        /// <param name="configure">
        ///  Configure an instance of the <see cref="FileLoggerOptions" /> to set logging options
        /// </param>
        [JetBrains.Annotations.NotNull]
        // ReSharper disable once UnusedMethodReturnValue.Global
        public static Microsoft.Extensions.Logging.ILoggingBuilder AddFile(
            [JetBrains.Annotations.NotNull] this Microsoft.Extensions.Logging.ILoggingBuilder builder, [JetBrains.Annotations.NotNull] System.Action<FileLoggerOptions> configure)
        {
            if (configure == null)
            {
                throw new System.ArgumentNullException(nameof(configure));
            }

            builder.AddFile();
            OptionsServiceCollectionExtensions.Configure(builder.Services, configure);

            return builder;
        }

        #endregion Public Methods
    }
}
