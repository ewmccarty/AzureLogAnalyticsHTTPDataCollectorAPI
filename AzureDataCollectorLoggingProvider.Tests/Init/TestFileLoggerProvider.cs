﻿namespace AzureDataCollectorLoggingProvider.Tests.Init
{
    internal sealed class TestFileLoggerProvider : RollingFileLogger.FileLoggerProvider
    {
        #region Public Constructors

        public TestFileLoggerProvider(
            //System.String customerId,
            //System.String sharedKey,
            //[JetBrains.Annotations.CanBeNull] System.String logName,
            [JetBrains.Annotations.CanBeNull] System.String path,
            [JetBrains.Annotations.CanBeNull] System.String fileName = "LogFile.",
            System.Int32 maxFileSize = 32_000,
            System.Int32 maxRetainedFiles = 100
        ) : base(new OptionsWrapper<RollingFileLogger.FileLoggerOptions>(new RollingFileLogger.FileLoggerOptions()
        {
            LogDirectory = path,
            FileName = fileName,
            FileSizeLimit = maxFileSize,
            RetainedFileCountLimit = maxRetainedFiles,
            IsEnabled = true
        }))
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
        }

        #endregion Public Constructors

        #region Internal Properties

        internal ManualIntervalControl IntervalControl { get; } = new ManualIntervalControl();

        #endregion Internal Properties

        #region Protected Methods

        [JetBrains.Annotations.NotNull]
        protected override System.Threading.Tasks.Task IntervalAsync(System.TimeSpan interval,
            System.Threading.CancellationToken cancellationToken)
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
            return this.IntervalControl.IntervalAsync();
        }

        #endregion Protected Methods
    }
}
