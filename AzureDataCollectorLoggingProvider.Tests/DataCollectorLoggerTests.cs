using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

// Microsoft.Extensions.Logging.AzureAppServices.AzureDataCollectorLogger
namespace AzureDataCollectorLoggingProvider.Tests
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public sealed class DataCollectorLoggerTests : TestContextBase, IDisposable
    {
        #region Private Fields

        // Update customerId to your Log Analytics workspace ID From https://portal.azure.com/#@cloudwalkertx.onmicrosoft.com/resource/subscriptions/1d77d23f-c627-409b-8cba-fbd9d6370dcf/resourceGroups/Log/providers/Microsoft.OperationalInsights/workspaces/WEIOMSWorkspace/Overview
        private const String CustomerId = "LogAnalyticsWorkSpaceID";

        // For sharedKey, use either the primary or the secondary Connected Sources client
        // authentication key
        // https://portal.azure.com/#@cloudwalkertx.onmicrosoft.com/resource/subscriptions/1d77d23f-c627-409b-8cba-fbd9d6370dcf/resourceGroups/Log/providers/Microsoft.OperationalInsights/workspaces/WEIOMSWorkspace/advancedSettings
        // Home > Log > WEIOMSWorkspace > Advanced settings > Connected Sources > Windows Servers
        private const String SharedKey = "WorkspaceConnectedSourcesAuthenticationKey";

        // LogType is name of the event type that is being submitted to Log Analytics ReSharper
        //
        // disable once InconsistentNaming
        private readonly String _logType =
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName.Replace(".", "_")}".RemoveAllSpecialCharacters();

        private Generic.LogMessage<ArgumentNullException> _logMessage1;
        private Generic.LogMessage<Exception> _logMessage2;
        [JetBrains.Annotations.UsedImplicitly] private Generic.LogMessage<String> _logMessage3;
        [JetBrains.Annotations.UsedImplicitly] private Generic.LogMessage<String> _logMessage4;
        [JetBrains.Annotations.UsedImplicitly] private DateTimeOffset _timestampOne = new DateTimeOffset(2016, 05, 04, 03, 02, 01, TimeSpan.Zero);

        #endregion Private Fields

        #region Public Methods

        public void Dispose()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local ReSharper disable once MemberCanBePrivate.Local

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitialize]
        public void Init()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            // ReSharper disable once LocalNameCapturedOnly
            String hr;
            var exception1 = new ArgumentNullException(nameof(hr), "LogMessageTestExceptionNoEvent Broke");

            var correlationId = Guid.NewGuid();

            this._logMessage1 = new Generic.LogMessage<ArgumentNullException>()
            {
                CorrelationId = correlationId,
                //EventId = new Microsoft.Extensions.Logging.EventId(101010);
                Exception = exception1,
                //Formatted = new System.Func<System.String, System.Exception, System.String>((state, ex) => this.GetType().ToString()),
                LogLevel = LogLevel.Trace,
                LogType =
                    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName.RemoveAllSpecialCharacters(),
                State = exception1,
                Timestamp = DateTime.Now
            };

            var exception2 = new Exception("This stupid thing broke!", exception1);

            this._logMessage2 = new Generic.LogMessage<Exception>()
            {
                CorrelationId = correlationId,
                //EventId = new Microsoft.Extensions.Logging.EventId(101010);
                Exception = exception2,
                // Formatted = new System.Func<System.String, System.Exception,
                // System.String>((state, ex) => this.GetType().ToString()),
                LogLevel = LogLevel.Trace,
                LogType =
                    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName.RemoveAllSpecialCharacters(),
                State = exception2,
                Timestamp = DateTime.Now
            };

            this._logMessage3 = new Generic.LogMessage<String>()
            {
                CorrelationId = correlationId,
                EventId = new EventId(101010),
                //Exception = exception,
                //    Formatted = new System.Func<System.String, System.Exception, System.String>((state, ex) => this.GetType().ToString()),
                LogLevel = LogLevel.Debug,
                LogType =
                    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName.RemoveAllSpecialCharacters(),
                State = "LogMessageTestInnerExceptionNoEvent System.String State",
                Timestamp = DateTime.Now
            };

            this._logMessage4 = new Generic.LogMessage<String>()
            {
                CorrelationId = correlationId,
                //EventId = new Microsoft.Extensions.Logging.EventId(101010),
                //Exception = exception,
                //Formatted = "",
                LogLevel = LogLevel.Debug,
                LogType =
                    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName.RemoveAllSpecialCharacters(),
                State = "LogMessageTestNoExceptionNoEvent State",
                Timestamp = DateTime.Now
            };
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public async Task WritesToAzureDataCollectorMessage1And2()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");

            var provider = new Init.TestDataCollectorLoggerProvider(DataCollectorLoggerTests.CustomerId, DataCollectorLoggerTests.SharedKey, this._logType);
            var logger = (DataCollectorLogger)provider.CreateLogger(this._logType);

            await provider.IntervalControl.Pause;

            logger.Log(this._logMessage1.LogLevel ?? LogLevel.Information, this._logMessage1.EventId ?? 0,
                this._logMessage1.State, this._logMessage1.Exception,
                (state, exception) => this._logMessage1.State.ToJson(), this._logMessage1.LogType, this._logMessage1.Timestamp, this._logMessage1.CorrelationId);

            logger.Log(this._logMessage2.LogLevel ?? LogLevel.Information, this._logMessage2.EventId ?? 0,
                this._logMessage2.State, this._logMessage2.Exception,
                (state, exception) => this._logMessage2.State.ToJson(), this._logMessage2.LogType, this._logMessage2.Timestamp, this._logMessage2.CorrelationId);

            provider.IntervalControl.Resume();
            //await provider.ProcessLogQueue();
            await provider.IntervalControl.Pause;

            provider.Stop();
            provider.Dispose();

            // TODO Read back the messages from the AzureDataCollectgorLogger
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(true);
        }

        // ReSharper disable once MemberCanBePrivate.Local
#pragma warning disable 628

        // ReSharper disable once MemberCanBeMadeStatic.Local
        protected void Dispose(Boolean disposing)
#pragma warning restore 628
        {
            if (disposing)
            {
            }
        }

        #endregion Public Methods
    }
}
