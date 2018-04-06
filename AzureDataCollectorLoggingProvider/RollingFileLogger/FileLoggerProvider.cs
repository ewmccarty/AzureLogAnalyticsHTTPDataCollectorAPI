using System.Linq;

// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable MemberCanBeInternal

// ReSharper disable UnusedTupleComponentInReturnValue

namespace AzureDataCollectorLoggingProvider.RollingFileLogger
{
    /// <inheritdoc />
    /// <summary>
    ///  An <see cref="T:Microsoft.Extensions.Logging.ILoggerProvider" /> that writes logs to a file
    /// </summary>
    [Microsoft.Extensions.Logging.ProviderAlias("File")]
    [JetBrains.Annotations.UsedImplicitly]
    // ReSharper disable once ClassCanBeSealed.Global
    public class FileLoggerProvider : BatchLogger.BatchingLoggerProvider
    {
        #region Public Constructors

        /// <summary>
        ///  Creates an instance of the /// <see cref="FileLoggerProvider" />
        /// </summary>
        /// <param name="options">The options object controlling the logger</param>
        public FileLoggerProvider([JetBrains.Annotations.NotNull] Microsoft.Extensions.Options.IOptions<FileLoggerOptions> options) : base(options)
        {
            var loggerOptions = options.Value;
            this.Path = loggerOptions.LogDirectory;
            this.FileName = loggerOptions.FileName;
            this.MaxFileSize = loggerOptions.FileSizeLimit;
            this.MaxRetainedFiles = loggerOptions.RetainedFileCountLimit;
        }

        #endregion Public Constructors

        #region Overrides of BatchingLoggerProvider

        protected internal override async System.Threading.Tasks.Task WriteMessagesAsync(
            System.Collections.Generic.IEnumerable<Generic.LogMessage<System.Object>> batchOfMessages,
            System.Threading.CancellationToken token)
        {
            System.IO.Directory.CreateDirectory(this.Path);

            foreach (IGrouping<(System.Int32 Year, System.Int32 Month, System.Int32 Day), Generic.LogMessage<System.Object>> group in batchOfMessages.GroupBy(FileLoggerProvider.GetGrouping))
            {
                var fullName = this.GetFullName(group.Key);
                var fileInfo = new System.IO.FileInfo(fullName);
                if (this.MaxFileSize > 0 && fileInfo.Exists && fileInfo.Length > this.MaxFileSize)
                {
                    return;
                }

                using (var streamWriter = System.IO.File.AppendText(fullName))
                {
                    foreach (var item in group)
                    {
                        await streamWriter.WriteAsync(item.Message);
                    }
                }
            }

            this.RollFiles();
        }

        #endregion Overrides of BatchingLoggerProvider

        #region Internal Fields

        /// <summary>
        ///  Log File Name
        /// </summary>
        // ReSharper disable MemberCanBePrivate.Global
        internal readonly System.String FileName;

        /// <summary>
        ///  Max Log file Size
        /// </summary>
        internal readonly System.Int32? MaxFileSize;

        /// <summary>
        ///  Maximum Number of Retained Log Files
        /// </summary>
        internal readonly System.Int32? MaxRetainedFiles;

        /// <summary>
        ///  Log File Path
        /// </summary>
        internal readonly System.String Path;

        #endregion Internal Fields

        // ReSharper restore MemberCanBePrivate.Global

        #region Protected Methods

        /// <summary>
        ///  Deletes old log files, keeping a number of files defined by /// <see cref="FileLoggerOptions.RetainedFileCountLimit" />
        /// </summary>
        protected internal void RollFiles()
        {
            if (!(this.MaxRetainedFiles > 0))
            {
                return;
            }

            System.Collections.Generic.IEnumerable<System.IO.FileInfo> files =
                new System.IO.DirectoryInfo(this.Path).GetFiles(this.FileName + "*").OrderByDescending(f => f.Name).Skip(this.MaxRetainedFiles.Value);

            foreach (var item in files)
            {
                item.Delete();
            }
        }

        #endregion Protected Methods

        #region Private Methods

        protected internal static (System.Int32 Year, System.Int32 Month, System.Int32 Day) GetGrouping(Generic.LogMessage<System.Object> message) =>
            (message.Timestamp.Year, message.Timestamp.Month, message.Timestamp.Day);

        protected internal System.String GetFullName((System.Int32 Year, System.Int32 Month, System.Int32 Day) group) =>
                            System.IO.Path.Combine(this.Path, $"{this.FileName}{group.Year:0000}{group.Month:00}{group.Day:00}.txt");

        #endregion Private Methods
    }
}
