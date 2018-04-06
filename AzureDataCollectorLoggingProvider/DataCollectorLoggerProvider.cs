/*
 * .NET Standard 2.0 Logger Provider for the Azure HTTP Data Collector API (public preview) in Log Analytics
 *
 * Derived from https://docs.microsoft.com/en-us/azure/log-analytics/log-analytics-data-collector-api
 * Derived from NetEscapades.Extensions.Logging.RollingFile
 * Erik w. McCarty 2018-03026
 */

// ReSharper disable InconsistentNaming
//
// ReSharper disable ClassCanBeSealed.Global
//
// ReSharper disable MemberCanBeInternal

using System.Linq;

// ReSharper disable MemberCanBePrivate.Global

namespace AzureDataCollectorLoggingProvider
{
    [JetBrains.Annotations.UsedImplicitly]
    public delegate System.String AzureDataCollectorFormatter<in TState>(
        [JetBrains.Annotations.CanBeNull] System.Guid? correlationId,
        [JetBrains.Annotations.CanBeNull] Microsoft.Extensions.Logging.EventId? eventId,
        [JetBrains.Annotations.CanBeNull] System.Exception exception,
        [JetBrains.Annotations.CanBeNull] Microsoft.Extensions.Logging.LogLevel? logLevel,
        [JetBrains.Annotations.CanBeNull] System.String logType, [JetBrains.Annotations.NotNull] TState state,
        [JetBrains.Annotations.CanBeNull] System.DateTimeOffset? timestamp);

    [Microsoft.Extensions.Logging.ProviderAlias("AzureDataCollector")]
    [JetBrains.Annotations.UsedImplicitly]
    public class DataCollectorLoggerProvider : RollingFileLogger.FileLoggerProvider, Microsoft.Extensions.Logging.ILoggerProvider
    // FileLogger.FileLoggerProvider
    {
        #region Public Methods

        /// <summary>
        ///  Adds a message to the _messageQueue
        /// </summary>
        /// <param name="message"></param>
        protected internal void AddMessage<TState>(Generic.LogMessage<TState> message)
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
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
                // TODO cancellation token canceled or CompleteAdding called
            }
        }

        #endregion Public Methods

        #region Implementation of IDisposable

        /// <inheritdoc />
        /// <summary>
        ///  Creates a new <see cref="T:Microsoft.Extensions.Logging.ILogger" /> instance.
        /// </summary>
        /// <param name="categoryNameLogType">
        ///  The category name for batchOfMessages produced by the logger.
        /// </param>
        /// <remarks><inheritdoc cref="T:Microsoft.Extensions.Logging.AzureAppServices.Internal.FileLoggerProvider" /></remarks>
        /// <returns></returns>
        [JetBrains.Annotations.NotNull]
        public new Microsoft.Extensions.Logging.ILogger CreateLogger(System.String categoryNameLogType)
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
            return new DataCollectorLogger(this, categoryNameLogType.RemoveAllSpecialCharacters());
        }

        /// <summary>
        ///  Creates a new <see cref="T:Microsoft.Extensions.Logging.ILogger" /> instance.
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <remarks><inheritdoc cref="T:Microsoft.Extensions.Logging.AzureAppServices.Internal.FileLoggerProvider" /></remarks>
        /// <returns></returns>
        [JetBrains.Annotations.NotNull]
        [JetBrains.Annotations.UsedImplicitly]
        public Microsoft.Extensions.Logging.ILogger CreateLogger<TState>()
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
            return new DataCollectorLogger(this, typeof(TState).FullName.RemoveAllSpecialCharacters());
        }

        /// <summary>
        ///  Performs application-defined tasks associated with freeing, releasing, or resetting
        ///  unmanaged resources.
        /// </summary>
        /// <remarks><inheritdoc cref="Microsoft.Extensions.Logging.AzureAppServices.Internal.FileLoggerProvider" /></remarks>
        [JetBrains.Annotations.UsedImplicitly]
        public new void Dispose()
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
            this.Stop();
            base.Dispose();
        }

        #endregion Implementation of IDisposable

        #region Private Fields

        /// <summary>
        ///  CustomerID The unique identifier for the Log Analytics workspace: Log Analytics
        ///  Workspace ID
        /// </summary>
        /// <remarks>
        ///  - See Log Analytics HTTP Data Collector API (https://docs.microsoft.com/en-us/azure/log-analytics/log-analytics-data-collector-api)
        ///  - Request URI parameters
        ///  - CustomerID The unique identifier for the Log Analytics workspace.
        /// </remarks>
        // ReSharper disable once MemberCanBePrivate.Global
        protected internal readonly System.String CustomerId;

        // ReSharper disable once MemberCanBePrivate.Global
        protected internal readonly System.String SharedKey;

        // ReSharper disable once MemberCanBePrivate.Global
        protected internal readonly System.TimeSpan WebTimeOut;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///  Creates an instance of the <see cref="Microsoft.Extensions.Logging.AzureAppServices.Internal.FileLoggerProvider" />
        /// </summary>
        /// <remarks><inheritdoc cref="Microsoft.Extensions.Logging.AzureAppServices.Internal.FileLoggerProvider" /></remarks>
        /// <param name="options">The options object controlling the logger</param>
        public DataCollectorLoggerProvider([JetBrains.Annotations.NotNull] Microsoft.Extensions.Options.IOptions<DataCollectorOptions> options) : base(options)
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
            var loggerOptions = options.Value;

            if (System.String.IsNullOrEmpty(loggerOptions.SharedKey))
            {
                throw new System.ArgumentNullException(nameof(options.Value.SharedKey), "The Azure Log Analytics Data Collector Workspace Connected Source Key cannot be null");
            }

            if (System.String.IsNullOrEmpty(loggerOptions.CustomerID))
            {
                throw new System.ArgumentNullException(nameof(options.Value.CustomerID), "The Azure Log Analytics Workspace ID (SharedKey) cannot be null");
            }

            this.SharedKey = loggerOptions.SharedKey;
            this.CustomerId = loggerOptions.CustomerID;
            this.WebTimeOut = loggerOptions.WebTimeOut ?? new System.TimeSpan(0, 0, 60);
            this.Start();
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        ///  Post the State
        /// </summary>
        /// <param name="batchOfMessages"></param>
        /// <param name="cancellationToken"></param>
        // ReSharper disable once UnusedParameter.Global
        protected internal new async System.Threading.Tasks.Task WriteMessagesAsync(System.Collections.Generic.IEnumerable<Generic.LogMessage<System.Object>> batchOfMessages, System.Threading.CancellationToken cancellationToken)
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif

            var groupOfMessagesHavingALogType = batchOfMessages.GroupBy(message => message.LogType);

            foreach (IGrouping<System.String, Generic.LogMessage<System.Object>> messages in groupOfMessagesHavingALogType)
            {
                var key = messages.Key;
                var json = messages.ToJson();
                // ReSharper disable once UnusedVariable
                var resp = await this.PostData(key, json, this.CustomerId, this.SharedKey);
#if DEBUG
                System.Console.WriteLine($"{{ \"LogType\" : \"{key}\", \r\n  \"messages\" : {json} }}");
#endif
            }
        }

        /// <summary>
        ///  Build the API authorization signature
        /// </summary>
        /// <param name="hashedString"></param>
        /// <param name="sharedKeySecret">
        ///  For sharedKeySecret, use either the primary or the secondary Connected Sources client
        ///  authentication key
        /// </param>
        /// <remarks>
        ///  - See Log Analytics HTTP Data Collector API (https://docs.microsoft.com/en-us/azure/log-analytics/log-analytics-data-collector-api)
        ///  - Request headers
        ///  - Authorization The authorization signature. Later in the article, you can read about
        ///    how to create an HMAC-SHA256 header.
        /// </remarks>
        private System.String BuildSignature(System.String hashedString, System.String sharedKeySecret)
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            System.Console.WriteLine($"\"sharedKeySecret\" : \"{sharedKeySecret}\",");
            System.Console.WriteLine($"\"hashedString\" : \"{hashedString}\" }}, ");
#endif

            var encoding = new System.Text.ASCIIEncoding();
            System.Byte[] keyByte = System.Convert.FromBase64String(sharedKeySecret);
            System.Byte[] messageBytes = encoding.GetBytes(hashedString);
            using (var hmacsha256 = new System.Security.Cryptography.HMACSHA256(keyByte))
            {
                System.Byte[] hash = hmacsha256.ComputeHash(messageBytes);
#if DEBUG
                System.Console.WriteLine($" }}");
#endif
                return System.Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        ///  Create a hash for the API signature
        /// </summary>
        /// <param name="message">JSON object, with key/value pairs</param>
        /// <param name="datestring"></param>
        /// <param name="customerId">Your Log Analytics workspace ID</param>
        /// <param name="sharedKey">
        ///  For sharedKeySecret, use either the primary or the secondary Connected Sources client
        ///  authentication key
        /// </param>
        /// <returns></returns>
        [JetBrains.Annotations.NotNull]
        private System.String GetAuthorizationHash(System.String message, System.String datestring,
            System.String customerId, System.String sharedKey)
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            System.Console.WriteLine($"\"customerId\" : \"{customerId}\",");
            System.Console.WriteLine($"\"datestring\" : \"{datestring}\",");
            System.Console.WriteLine($"\"sharedKeySecret\" : \"{sharedKey}\",");
            System.Console.WriteLine($"\"message\" : {{ {message}  }}, ");
#endif

            // Create a hash for the API authorization
            System.Byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(message);
            var stringToHash = "POST\n" + jsonBytes.Length + "\napplication/json\n" + "x-ms-date:" + datestring +
                "\n/api/logs";
            var hashedString = this.BuildSignature(stringToHash, sharedKey);
            // yes, this is confirmed to be the customerId
            var authorization = "SharedKey " + customerId + ":" + hashedString;

#if DEBUG
            System.Console.WriteLine($"\"hashedString\" : \"{hashedString}\",");
            System.Console.WriteLine($"\"authorization\" : \"{authorization}\",");
            System.Console.WriteLine($"}}");
#endif

            return authorization;
        }

        /// <summary>
        ///  Send a request to the POST API endpoint
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        /// <param name="customerId"></param>
        /// <param name="sharedKey"></param>
        // ReSharper disable once MemberCanBePrivate.Global
        [JetBrains.Annotations.UsedImplicitly]
        private async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> PostData(System.DateTimeOffset timestamp, System.String logType, System.String message, System.String customerId, System.String sharedKey)

        {
            var url = $"https://{customerId}.ods.opinsights.azure.com/api/logs?api-version=2016-04-01";
#if DEBUG
            System.Console.WriteLine($"{{ \"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}\" :  {{");
            System.Console.WriteLine($"\"customerId\" : \"{customerId}\",");
            System.Console.WriteLine($"\"logType\" : \"{logType}\",");
            System.Console.WriteLine($"\"sharedKeySecret\" : \"{sharedKey}\",");
            System.Console.WriteLine($"\"timestamp\" : \"{timestamp}\",");
            System.Console.WriteLine($"\"message\" : {{ {message}  }}, ");
            System.Console.WriteLine(url);
#endif

            var response =
                new System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage>(() => null);
            var datestring = System.DateTime.UtcNow.ToString("r");
            var authorization = this.GetAuthorizationHash(message, datestring, customerId, sharedKey);
            var client = new System.Net.Http.HttpClient
            {
                Timeout = this.WebTimeOut
            };
            if (!Connectivity.NetworkStatus.IsAvailable)
            {
                var err = new System.ApplicationException("The Internet is not available");
                throw err;
            }
            else
            {
                try
                {
                    // Note the formatting of the datestring vs timestamp

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Log-Type", logType);
                    client.DefaultRequestHeaders.Add("Authorization", authorization);
                    client.DefaultRequestHeaders.Add("x-ms-date", datestring);

                    // let the Log Analytics HTTP DataCollector API assign the Timestamp
                    client.DefaultRequestHeaders.Add("time-generated-field", timestamp.ToIso8601());

                    System.Net.Http.HttpContent httpContent = new System.Net.Http.StringContent(message, System.Text.Encoding.UTF8);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    response = client.PostAsync(new System.Uri(url), httpContent);
#if DEBUG
                    System.Console.WriteLine($"\"PostDate response\": {{ {response.ToJson()} }}");
#endif
                }
#pragma warning disable 168
                catch (System.Exception excep)
#pragma warning restore 168
                {
                    var stateMsg =
                        $"Error E00650 in \"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}\" sending \"{message.ToJson()}\" to \"{url}\"";
                    // ReSharper disable once UnusedVariable
                    var ex = new System.Exception(stateMsg, excep);
                    var logMessage = new Generic.LogMessage<System.String>(Microsoft.Extensions.Logging.LogLevel.Error, Events.E00650, stateMsg, excep, this.AzureDataCollectorFormatter);
                    this.AddMessage(logMessage);
#if DEBUG
                    System.Console.WriteLine($"\"PostDate Exception\": {{ {excep.ToJson()} }}");
#endif
                }
#if DEBUG
                finally
                {
                    System.Console.WriteLine($"}}");
                }
#endif
                return await response;
            }
        }

        /// <summary>
        ///  Send a request to the POST API endpoint
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        /// <param name="customerId"></param>
        /// <param name="sharedKey"></param>
        // ReSharper disable once MemberCanBePrivate.Global
        private async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> PostData(System.String logType, System.String message, System.String customerId, System.String sharedKey)
        {
            var url = $"https://{customerId}.ods.opinsights.azure.com/api/logs?api-version=2016-04-01";
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} : {{");
            System.Console.WriteLine($"\"customerId\" : \"{customerId}\",");
            System.Console.WriteLine($"\"logType\" : \"{logType}\",");
            System.Console.WriteLine($"\"sharedKeySecret\" : \"{sharedKey}\",");
            System.Console.WriteLine($"\"message\" : {{ {message}  }}, ");
            System.Console.WriteLine($"\"url\" : \"{url}\" , ");
#endif

            var response =
                new System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage>(() => null);
            // Note the formatting of the datestring vs timestamp
            var datestring = System.DateTime.UtcNow.ToString("r");
            var authorization = this.GetAuthorizationHash(message, datestring, customerId, sharedKey);

            var client = new System.Net.Http.HttpClient
            {
                Timeout = this.WebTimeOut
            };
            try
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Log-Type", logType);
                client.DefaultRequestHeaders.Add("Authorization", authorization);
                client.DefaultRequestHeaders.Add("x-ms-date", datestring);

                // let the Log Analytics HTTP DataCollector API assign the Timestamp
                //System.Console.WriteLine(Program.TimeStampField);
                //client.DefaultRequestHeaders.Add("time-generated-field", Program.TimeStampField);

                System.Net.Http.HttpContent httpContent = new System.Net.Http.StringContent(message, System.Text.Encoding.UTF8);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                response = client.PostAsync(new System.Uri(url), httpContent);

#if DEBUG
                System.Console.WriteLine($"\"PostDate response\": {{ {response.ToJson()} }}");
#endif
            }
            catch (System.Exception excep)
            {
                var stateMsg = $"Error E00696 in \"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}\" sending \"{message.ToJson()}\" to \"{url}\"";
                // ReSharper disable once UnusedVariable
                var ex = new System.Exception(stateMsg, excep);
                var logMessage = new Generic.LogMessage<System.String>(Microsoft.Extensions.Logging.LogLevel.Error, Events.E00696, stateMsg, excep, this.AzureDataCollectorFormatter);
                this.AddMessage(logMessage);
#if DEBUG
                System.Console.WriteLine($"\"PostDate Exception\": {{ {excep.ToJson()} }}");
#endif
            }
#if DEBUG
            finally
            {
                System.Console.WriteLine($"}}");
            }
#endif
            return await response;
        }

        #endregion Private Methods

        public System.String AzureDataCollectorFormatter<TState>(
            [JetBrains.Annotations.NotNull] TState state,
            [JetBrains.Annotations.CanBeNull] System.Exception exception
        ) =>
            this.AzureDataCollectorFormatter(null, null, exception, null, null, state, null);

        public System.String AzureDataCollectorFormatter<TState>(
            [JetBrains.Annotations.CanBeNull] System.Guid? correlationId,
            [JetBrains.Annotations.CanBeNull] Microsoft.Extensions.Logging.EventId? eventId,
            [JetBrains.Annotations.CanBeNull] System.Exception exception,
            [JetBrains.Annotations.CanBeNull] Microsoft.Extensions.Logging.LogLevel? logLevel,
            [JetBrains.Annotations.CanBeNull] System.String logType,
            [JetBrains.Annotations.NotNull] TState state,
            [JetBrains.Annotations.CanBeNull] System.DateTimeOffset? timestamp
        )
        {
            var m = new Generic.LogMessage<TState>
            {
                CorrelationId = correlationId ?? System.Guid.NewGuid(),
                EventId = eventId,
                Exception = exception,
                Formatter = this.AzureDataCollectorFormatter,
                LogLevel = logLevel ?? ((exception == null) ? Microsoft.Extensions.Logging.LogLevel.Information : Microsoft.Extensions.Logging.LogLevel.Error),
                LogType = logType ?? typeof(TState).FullName.RemoveAllSpecialCharacters(),
                State = state,
                Timestamp = timestamp ?? System.DateTimeOffset.Now
            };
            return m.ToJson();
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public new async System.Threading.Tasks.Task ProcessLogQueue()
        {
#if DEBUG
            System.Console.WriteLine($"{{ \"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}\" : {{ ");
#endif
            while (!this.CancellationTokenSource.IsCancellationRequested)
            {
                var limit = this.BatchSize ?? System.Int32.MaxValue;

                while (limit > 0 && this.MessageQueue.TryTake(out var message))
                {
                    this.CurrentBatch.Add(message);
                    limit--;
                }

                var currentBatchCount = this.CurrentBatch.Count;
#if DEBUG
                System.Console.WriteLine($"\"currentBatchCount\" : {currentBatchCount} , ");
#endif
                if (currentBatchCount > 0)
                {
#if DEBUG
                    System.Console.WriteLine($"\"currentBatchCount\" : {currentBatchCount} , ");
#endif

                    try
                    {
                        await this.WriteMessagesAsync(this.CurrentBatch, this.CancellationTokenSource.Token);
                    }
                    catch (System.Exception excep)
                    {
                        // Write to the Azure App Services Data Collector fails, redirecting messages
                        // to the base FileLogger
                        var stateMsg = $"Error 000793 in \"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}\" sending current batch count of \"{currentBatchCount}messages to WriteMessagesAsync()";
                        var logMessage = new Generic.LogMessage<System.String>(Microsoft.Extensions.Logging.LogLevel.Error, Events.E00793, stateMsg, excep, this.AzureDataCollectorFormatter);
                        this.AddMessage(logMessage);
                        await base.WriteMessagesAsync(this.CurrentBatch, this.CancellationTokenSource.Token);
#if DEBUG
                        System.Console.WriteLine($"\"error\" : \"this.WriteMessagesAsync error in ProcessLogQueue\"");
#endif
                    }
#if DEBUG
                    finally
                    {
                        System.Console.WriteLine($"\"success\" : \"this.WriteMessagesAsync success in ProcessLogQueue\"");
                    }
#endif

                    this.CurrentBatch.Clear();
                }

                await this.IntervalAsync(this.Interval, this.CancellationTokenSource.Token);
            }
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public new void Start()
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
            this.MessageQueue = this.QueueSize == null
                ? new System.Collections.Concurrent.BlockingCollection<Generic.LogMessage<System.Object>>(
                    new System.Collections.Concurrent.ConcurrentQueue<Generic.LogMessage<System.Object>>())
                : new System.Collections.Concurrent.BlockingCollection<Generic.LogMessage<System.Object>>(
                    new System.Collections.Concurrent.ConcurrentQueue<Generic.LogMessage<System.Object>>(), this.QueueSize.Value);

            this.CancellationTokenSource = new System.Threading.CancellationTokenSource();
            this.OutputTask = System.Threading.Tasks.Task.Factory.StartNew(state => this.ProcessLogQueue(), null,
                System.Threading.Tasks.TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public new void Stop()
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
            this.CancellationTokenSource.Cancel();
            this.MessageQueue.CompleteAdding();

            try
            {
                this.OutputTask.Wait(this.Interval);
            }
            catch (System.Threading.Tasks.TaskCanceledException excep)
            {
                throw new System.Threading.Tasks.TaskCanceledException(
                    $"Error 000853 in \"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}\" attempting STOP()",
                    excep);
            }
            catch (System.AggregateException ex) when (ex.InnerExceptions.Count == 1 && ex.InnerExceptions[0] is System.Threading.Tasks.TaskCanceledException)
            {
                throw new System.AggregateException(
                    $"Error 000853 in \"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}\" attempting STOP()",
                    ex);
            }
        }

        protected internal override System.Threading.Tasks.Task IntervalAsync(System.TimeSpan interval,
                    System.Threading.CancellationToken cancellationToken)
        {
#if DEBUG
            System.Console.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
#endif
            return System.Threading.Tasks.Task.Delay(interval, cancellationToken);
        }
    }
}
