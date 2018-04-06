namespace AzureDataCollectorLoggingProvider.Interfaces.RollingFileLogger
{
    public interface IFileLoggerOptions : BatchLogger.IBatchingLoggerOptions

    {
        #region Public Properties

        /// <summary>
        ///  Gets or sets the filename prefix to use for log files. Defaults to <c>logs-</c>.
        /// </summary>
        System.String FileName { get; set; }

        /// <summary>
        ///  Gets or sets a strictly positive value representing the maximum log size in bytes or
        ///  null for no limit. Once the log is full, no more messages will be appended. Defaults to <c>10MB</c>.
        /// </summary>
        System.Int32? FileSizeLimit { get; set; }

        /// <summary>
        ///  The directory in which log files will be written, relative to the app process. Default
        ///  to <c>Logs</c>
        /// </summary>
        /// <returns></returns>
        System.String LogDirectory { get; set; }

        /// <summary>
        ///  Gets or sets a strictly positive value representing the maximum retained file count or
        ///  null for no limit. Defaults to <c>2</c>.
        /// </summary>
        System.Int32? RetainedFileCountLimit { get; set; }

        #endregion Public Properties
    }
}
