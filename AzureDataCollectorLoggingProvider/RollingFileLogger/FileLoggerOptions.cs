// ReSharper disable ClassCanBeSealed.Global ReSharper disable MemberCanBeInternal ReSharper disable
// MemberCanBePrivate.Global ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
namespace AzureDataCollectorLoggingProvider.RollingFileLogger
{
    public class FileLoggerOptions : BatchLogger.BatchingLoggerOptions, Interfaces.RollingFileLogger.IFileLoggerOptions
    {
        #region Private Fields

        private System.String _fileName = "logs-";
        private System.Int32? _fileSizeLimit = 10 * 1024 * 1024;
        private System.Int32? _retainedFileCountLimit = 2;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        ///  Gets or sets the filename prefix to use for log files. Defaults to <c>logs-</c>.
        /// </summary>
        public System.String FileName
        {
            get => this._fileName;
            set
            {
                if (System.String.IsNullOrEmpty(value))
                {
                    throw new System.ArgumentException(nameof(value));
                }

                this._fileName = value;
            }
        }

        /// <summary>
        ///  Gets or sets a strictly positive value representing the maximum log size in bytes or
        ///  null for no limit. Once the log is full, no more messages will be appended. Defaults to <c>10MB</c>.
        /// </summary>
        public System.Int32? FileSizeLimit
        {
            get => this._fileSizeLimit;
            set
            {
                if (value <= 0)
                {
                    throw new System.ArgumentOutOfRangeException(nameof(value),
                        $"{nameof(FileLoggerOptions.FileSizeLimit)} must be positive.");
                }

                this._fileSizeLimit = value;
            }
        }

        /// <summary>
        ///  The directory in which log files will be written, relative to the app process. Default
        ///  to <c>Logs</c>
        /// </summary>
        /// <returns></returns>
        public System.String LogDirectory { get; set; } = "Logs";

        /// <summary>
        ///  Gets or sets a strictly positive value representing the maximum retained file count or
        ///  null for no limit. Defaults to <c>2</c>.
        /// </summary>
        public System.Int32? RetainedFileCountLimit
        {
            get => this._retainedFileCountLimit;
            set
            {
                if (value <= 0)
                {
                    throw new System.ArgumentOutOfRangeException(nameof(value),
                        $"{nameof(FileLoggerOptions.RetainedFileCountLimit)} must be positive.");
                }

                this._retainedFileCountLimit = value;
            }
        }

        #endregion Public Properties
    }
}
