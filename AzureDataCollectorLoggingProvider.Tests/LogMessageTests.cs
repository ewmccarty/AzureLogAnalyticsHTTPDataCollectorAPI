using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AzureDataCollectorLoggingProvider.Tests
{
    [TestClass]
    public sealed class LogMessageTests : TestContextBase
    {
        #region Private Fields

        private Interfaces.Generic.ILogMessage<System.ArgumentNullException> _logMessage1;
        private Interfaces.Generic.ILogMessage<System.Exception> _logMessage2;
        private Interfaces.Generic.ILogMessage<System.String> _logMessage3;
        private Interfaces.Generic.ILogMessage<System.String> _logMessage4;

        #endregion Private Fields

        #region Public Methods

        [TestInitialize]
        public void Init()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            // ReSharper disable once LocalNameCapturedOnly
            System.String hr;
            var exception1 = new System.ArgumentNullException(nameof(hr), "LogMessageTestExceptionNoEvent Broke");

            var correlationId = System.Guid.NewGuid();

            this._logMessage1 = new Generic.LogMessage<System.ArgumentNullException>()
            {
                CorrelationId = correlationId,
                //EventId = new Microsoft.Extensions.Logging.EventId(101010);
                Exception = exception1,
                //Formatted = new System.Func<System.String, System.Exception, System.String>((state, ex) => this.GetType().ToString()),
                LogLevel = Microsoft.Extensions.Logging.LogLevel.Trace,
                LogType =
                    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName.RemoveAllSpecialCharacters(),
                State = exception1,
                Timestamp = System.DateTime.Now
            };

            var exception2 = new System.Exception("This stupid thing broke!", exception1);

            this._logMessage2 = new Generic.LogMessage<System.Exception>()
            {
                CorrelationId = correlationId,
                //EventId = new Microsoft.Extensions.Logging.EventId(101010);
                Exception = exception2,
                // Formatted = new System.Func<System.String, System.Exception,
                // System.String>((state, ex) => this.GetType().ToString()),
                LogLevel = Microsoft.Extensions.Logging.LogLevel.Trace,
                LogType =
                    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName.RemoveAllSpecialCharacters(),
                State = exception2,
                Timestamp = System.DateTime.Now
            };

            this._logMessage3 = new Generic.LogMessage<System.String>()
            {
                CorrelationId = correlationId,
                EventId = new Microsoft.Extensions.Logging.EventId(101010),
                //Exception = exception,
                //    Formatted = new System.Func<System.String, System.Exception, System.String>((state, ex) => this.GetType().ToString()),
                LogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
                LogType =
                    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName.RemoveAllSpecialCharacters(),
                State = "LogMessageTestInnerExceptionNoEvent System.String State",
                Timestamp = System.DateTime.Now
            };

            this._logMessage4 = new Generic.LogMessage<System.String>()
            {
                CorrelationId = correlationId,
                //EventId = new Microsoft.Extensions.Logging.EventId(101010),
                //Exception = exception,
                //Formatted = "",
                LogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
                LogType =
                    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName.RemoveAllSpecialCharacters(),
                State = "LogMessageTestNoExceptionNoEvent State",
                Timestamp = System.DateTime.Now
            };
        }

        #endregion Public Methods

        #region Public Methods

        /// <summary>
        ///  Verify the Init was ok
        /// </summary>
        /// <remarks>Seems a stupid test, but was actually diagnostic at one point</remarks>
        [TestMethod]
        public void LogMessageInitTest()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            Assert.IsTrue(true, "SUCCESS: Init didn't blow up");
        }

        /// <summary>
        ///  Verify the message is ok
        /// </summary>
        /// <remarks>Seems a stupid test, but was actually diagnostic at one point</remarks>
        [TestMethod]
        public void LogMessageTestExceptionNoEvent0()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            // ReSharper disable once UnusedVariable
            var a = this._logMessage1;
            Assert.IsTrue(true, "SUCCESS: Init didn't blow up");
        }

        /// <summary>
        ///  test a message as manual json
        /// </summary>
        /// <remarks>Seems a stupid test, but was actually diagnostic at one point</remarks>
        [TestMethod]
        public void LogMessageTestExceptionNoEvent1()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            var a = this._logMessage1;
            var json = $"{{ " + $"\r\n " + "\t" + $"\"CorrelationId\" : \"{a.CorrelationId}\", " + $"\r\n " + "\t" +
                $"\"EventId\" : \"{a.EventId}\", " + $"\r\n " + "\t" + $"\"Exception\" : {a.Exception.ToJson()}, " +
                $"\r\n " + "\t" + $"\"Formatter\" : \"{a.Formatter}\", " + $"\r\n " + "\t" + $"\"LogLevel\" : \"{a.LogLevel}\", " + $"\r\n " + "\t" +
                $"\"LogType\" : \"{a.LogType}\", " + $"\r\n " + "\t" + $"\"State\" : \"{a.State}\", " + $"\r\n " + "\t" +
                $"\"Timestamp\" : \"{a.Timestamp}\" " + $"\r\n " + $"}}";
            this.TestContext.WriteLine(json);
            Assert.IsTrue(true, "SUCCESS: Init didn't blow up");
        }

        /// <summary>
        ///  Test conversion to JSON
        /// </summary>
        /// <remarks>Seems a stupid test, but was actually diagnostic at one point</remarks>
        [TestMethod]
        public void LogMessageTestExceptionNoEvent2()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            var a = this._logMessage1;
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(a, Newtonsoft.Json.Formatting.Indented,
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    //Newtonsoft.Json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                    PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None,
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    TypeNameAssemblyFormatHandling = Newtonsoft.Json.TypeNameAssemblyFormatHandling.Simple,
                    TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects
                });
            this.TestContext.WriteLine(json);
            Assert.IsTrue(true, "SUCCESS: Init didn't blow up");
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void LogMessageTestExceptionNoEvent3()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            // ReSharper disable once UnusedVariable
            var a = this._logMessage1.ToJson();
            Assert.IsTrue(true, "SUCCESS: Init didn't blow up");
        }

        /// <summary>
        ///  Check verify no issues convering successively more complex messages to json
        /// </summary>
        [TestMethod]
        public void LogMessageTestExceptionNoEvent4()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            this.TestContext.WriteLine(this._logMessage1.ToJson());
            Assert.IsTrue(true, "SUCCESS: Init didn't blow up");
        }

        /// <summary>
        ///  Check verify no issues convering successively more complex messages to json
        /// </summary>
        [TestMethod]
        public void LogMessageTestInnerExceptionNoEvent()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            this.TestContext.WriteLine(this._logMessage2.ToJson());
            Assert.IsTrue(true, "SUCCESS: Init didn't blow up");
        }

        /// <summary>
        ///  Check verify no issues convering successively more complex messages to json
        /// </summary>
        [TestMethod]
        public void LogMessageTestNoExceptionEvent()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            this.TestContext.WriteLine(this._logMessage3.ToJson());
            Assert.IsTrue(true, "SUCCESS: Init didn't blow up");
        }

        /// <summary>
        ///  Check verify no issues convering successively more complex messages to json
        /// </summary>
        [TestMethod]
        public void LogMessageTestNoExceptionNoEvent()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            this.TestContext.WriteLine(this._logMessage4.ToJson());
            Assert.IsTrue(true, "SUCCESS: Init didn't blow up");
        }

        #endregion Public Methods
    }
}
