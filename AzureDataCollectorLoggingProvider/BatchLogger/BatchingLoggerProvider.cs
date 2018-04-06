// ReSharper disable MemberCanBePrivate.Global ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBeProtected.Global
namespace AzureDataCollectorLoggingProvider.BatchLogger
{
    [Microsoft.Extensions.Logging.ProviderAlias("BatchingLogger")]
    public abstract class BatchingLoggerProvider : Interfaces.BatchLogger.IBatchingLoggerProvider
    {
        #region Public Methods

        protected internal abstract System.Threading.Tasks.Task WriteMessagesAsync(
            [JetBrains.Annotations.UsedImplicitly] System.Collections.Generic.IEnumerable<Generic.LogMessage<System.Object>> batchOfMessages, System.Threading.CancellationToken token);

        #endregion Public Methods

        #region Protected Constructors

        protected internal BatchingLoggerProvider([JetBrains.Annotations.NotNull] Microsoft.Extensions.Options.IOptions<BatchingLoggerOptions> options)
        {
            // NOTE: Only IsEnabled is monitored

            var loggerOptions = options.Value;
            if (loggerOptions.BatchSize <= 0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(loggerOptions.BatchSize),
                    $"{nameof(loggerOptions.BatchSize)} must be a positive number.");
            }

            if (loggerOptions.FlushPeriod <= System.TimeSpan.Zero)
            {
                throw new System.ArgumentOutOfRangeException(nameof(loggerOptions.FlushPeriod),
                    $"{nameof(loggerOptions.FlushPeriod)} must be longer than zero.");
            }

            this.Interval = loggerOptions.FlushPeriod;
            this.BatchSize = loggerOptions.BatchSize;
            this.QueueSize = loggerOptions.BackgroundQueueSize;

            this.Start();
        }

        #endregion Protected Constructors

        #region ILoggerProvider Members

        [JetBrains.Annotations.NotNull]
        public Microsoft.Extensions.Logging.ILogger CreateLogger(System.String categoryName) =>
            new BatchingLogger(this, categoryName);

        public void Dispose()
        {
            this.Stop();
        }

        #endregion ILoggerProvider Members

        #region Internal Methods

        protected internal void AddMessage(Generic.LogMessage<System.Object> message)
        {
            if (this.MessageQueue.IsAddingCompleted)
            {
                return;
            }

            try
            {
                this.MessageQueue.Add(message, this.CancellationTokenSource.Token);
            }
            catch
            {
                //TODO cancellation token canceled or CompleteAdding called
            }
        }

        #endregion Internal Methods

        #region Internal Fields

        /// <summary>
        ///  Initialized in BatchLoggingProvider protected Constructor
        /// </summary>
        internal readonly System.Int32? BatchSize;

        internal readonly System.Collections.Generic.IList<Generic.LogMessage<System.Object>> CurrentBatch =
                    new System.Collections.Generic.List<Generic.LogMessage<System.Object>>();

        /// <summary>
        ///  Initialized in BatchLoggingProvider protected Constructor
        /// </summary>
        internal readonly System.TimeSpan Interval;

        /// <summary>
        ///  Initialized in BatchLoggingProvider protected Constructor
        /// </summary>
        internal readonly System.Int32? QueueSize;

        internal System.Threading.CancellationTokenSource CancellationTokenSource;

        internal System.Collections.Concurrent.BlockingCollection<Generic.LogMessage<System.Object>> MessageQueue;
        internal System.Threading.Tasks.Task OutputTask;

        #endregion Internal Fields

        #region Protected Methods

        protected internal virtual System.Threading.Tasks.Task IntervalAsync(System.TimeSpan interval,
            System.Threading.CancellationToken cancellationToken) =>
            System.Threading.Tasks.Task.Delay(interval, cancellationToken);

        #endregion Protected Methods

        #region Private Methods

        protected internal async System.Threading.Tasks.Task ProcessLogQueue()
        {
            while (!this.CancellationTokenSource.IsCancellationRequested)
            {
                var limit = this.BatchSize ?? System.Int32.MaxValue;

                while (limit > 0 && this.MessageQueue.TryTake(out var message))
                {
                    this.CurrentBatch.Add(message);
                    limit--;
                }

                if (this.CurrentBatch.Count > 0)
                {
                    try
                    {
                        await this.WriteMessagesAsync(this.CurrentBatch, this.CancellationTokenSource.Token);
                    }
                    catch
                    {
                        // ignored
                    }

                    this.CurrentBatch.Clear();
                }

                await this.IntervalAsync(this.Interval, this.CancellationTokenSource.Token);
            }
        }

        protected internal void Start()
        {
            this.MessageQueue = this.QueueSize == null
                ? new System.Collections.Concurrent.BlockingCollection<Generic.LogMessage<System.Object>>(
                    new System.Collections.Concurrent.ConcurrentQueue<Generic.LogMessage<System.Object>>())
                : new System.Collections.Concurrent.BlockingCollection<Generic.LogMessage<System.Object>>(
                    new System.Collections.Concurrent.ConcurrentQueue<Generic.LogMessage<System.Object>>(), this.QueueSize.Value);

            this.CancellationTokenSource = new System.Threading.CancellationTokenSource();
            this.OutputTask = System.Threading.Tasks.Task.Factory.StartNew(state => this.ProcessLogQueue(), null,
                System.Threading.Tasks.TaskCreationOptions.LongRunning);
        }

        protected internal void Stop()
        {
            this.CancellationTokenSource.Cancel();
            this.MessageQueue.CompleteAdding();

            try
            {
                this.OutputTask.Wait(this.Interval);
            }
            catch (System.Threading.Tasks.TaskCanceledException)
            {
            }
            catch (System.AggregateException ex) when (ex.InnerExceptions.Count == 1 &&
                ex.InnerExceptions[0] is System.Threading.Tasks.TaskCanceledException)
            {
            }
        }

        #endregion Private Methods
    }
}
