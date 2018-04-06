namespace AzureDataCollectorLoggingProvider.BatchLogger
{
    public class BatchingLoggerOptions : Interfaces.BatchLogger.IBatchingLoggerOptions
    {
        #region Private Fields

        private System.Int32? _backgroundQueueSize;
        private System.Int32? _batchSize = 32;
        private System.TimeSpan _flushPeriod = System.TimeSpan.FromSeconds(1);

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        ///  Gets or sets the maximum size of the background log message queue or null for no limit.
        ///  After maximum queue size is reached log event sink would start blocking. Defaults to <c>null</c>.
        /// </summary>
        public System.Int32? BackgroundQueueSize
        {
            get => this._backgroundQueueSize;
            set
            {
                if (value < 0)
                {
                    throw new System.ArgumentOutOfRangeException(nameof(value),
                        $"{nameof(BatchingLoggerOptions.BackgroundQueueSize)} must be non-negative.");
                }

                this._backgroundQueueSize = value;
            }
        }

        /// <summary>
        ///  Gets or sets a maximum number of events to include in a single batch or null for no limit.
        /// </summary>
        public System.Int32? BatchSize
        {
            get => this._batchSize;
            set
            {
                if (value <= 0)
                {
                    throw new System.ArgumentOutOfRangeException(nameof(value),
                        $"{nameof(BatchingLoggerOptions.BatchSize)} must be positive.");
                }

                this._batchSize = value;
            }
        }

        /// <summary>
        ///  Gets or sets the period after which logs will be flushed to the store.
        /// </summary>
        public System.TimeSpan FlushPeriod
        {
            get => this._flushPeriod;
            set
            {
                if (value <= System.TimeSpan.Zero)
                {
                    throw new System.ArgumentOutOfRangeException(nameof(value),
                        $"{nameof(BatchingLoggerOptions.FlushPeriod)} must be positive.");
                }

                this._flushPeriod = value;
            }
        }

        /// <summary>
        ///  Gets or sets value indicating if logger accepts and queues writes.
        /// </summary>
        public System.Boolean IsEnabled { get; set; }

        #endregion Public Properties
    }
}
