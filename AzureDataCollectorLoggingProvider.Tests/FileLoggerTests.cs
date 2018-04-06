using System.Linq;

namespace AzureDataCollectorLoggingProvider.Tests
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public sealed class FileLoggerTests : TestContextBase, System.IDisposable
    {
        #region Private Fields

        private System.DateTimeOffset _timestampOne =
            new System.DateTimeOffset(2016, 05, 04, 03, 02, 01, System.TimeSpan.Zero);

        #endregion Private Fields

        #region Public Constructors

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitialize]
        public void InitFileLoggerTests()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            this.TempPath = System.IO.Path.GetTempFileName() + "_";
        }

        #endregion Public Constructors

        #region Public Properties

        // ReSharper disable once MemberCanBePrivate.Global
        public System.String TempPath { get; set; }

        #endregion Public Properties

        #region Public Methods

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanup]
        public void Dispose()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            try
            {
                if (System.IO.Directory.Exists(this.TempPath))
                {
                    System.IO.Directory.Delete(this.TempPath, true);
                }
            }
            catch
            {
                // ignored
            }
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public async System.Threading.Tasks.Task RespectsMaxFileCount()
        {
            this.TestContext.WriteLine($"{{ \r\n \"Location\" : \"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}\", ");

            this.TestContext.WriteLine($"\"TempPath\" : \"{this.TempPath}\", ");

            System.IO.Directory.CreateDirectory(this.TempPath);
            System.IO.File.WriteAllText(System.IO.Path.Combine(this.TempPath, "randomFile.txt"), "");

            var provider = new Init.TestFileLoggerProvider(this.TempPath, maxRetainedFiles: 5);
            var logger = (BatchLogger.BatchingLogger)provider.CreateLogger("Cat");
            var correlationId = new System.Guid("939221c1-c966-4762-8d6c-77b1e3fe29e3");

            await provider.IntervalControl.Pause;
            var timestamp = this._timestampOne;

            for (var i = 0; i < 10; i++)
            {
                logger.Log(timestamp, Microsoft.Extensions.Logging.LogLevel.Information, 0, "Info message", null,
                    (state, ex) => state, correlationId);
                logger.Log(timestamp.AddHours(1), Microsoft.Extensions.Logging.LogLevel.Error, 0, "Error message", null,
                    (state, ex) => state, correlationId);

                timestamp = timestamp.AddDays(1);
            }

            provider.IntervalControl.Resume();

            await provider.IntervalControl.Pause;

            System.String[] actualFiles = new System.IO.DirectoryInfo(this.TempPath).GetFiles().Select(f => f.Name).OrderBy(f => f).ToArray();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(6, actualFiles.Length);
            this.TestContext.WriteLine(actualFiles.ToJson());
            var expected = new[]
            {
                "LogFile.20160509.txt", "LogFile.20160510.txt", "LogFile.20160511.txt", "LogFile.20160512.txt",
                "LogFile.20160513.txt", "randomFile.txt"
            };

            this.TestContext.WriteLine(actualFiles.ToJson());
            this.TestContext.WriteLine($"{expected[0]}:{actualFiles[0]}");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected[0], actualFiles[0]);
            this.TestContext.WriteLine($"{expected[1]}:{actualFiles[1]}");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected[1], actualFiles[1]);
            this.TestContext.WriteLine($"{expected[2]}:{actualFiles[2]}");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected[2], actualFiles[2]);
            this.TestContext.WriteLine($"{expected[3]}:{actualFiles[3]}");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected[3], actualFiles[3]);
            this.TestContext.WriteLine($"{expected[4]}:{actualFiles[4]}");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected[4], actualFiles[4]);
            this.TestContext.WriteLine($"{expected[5]}:{actualFiles[5]}");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected[5], actualFiles[5]);
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public async System.Threading.Tasks.Task RollsTextFile()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            var provider = new Init.TestFileLoggerProvider(this.TempPath);
            var logger = (BatchLogger.BatchingLogger)provider.CreateLogger("Cat");

            await provider.IntervalControl.Pause;
            var correlationId1 = new System.Guid("0496227e-8499-45fb-bbde-165e6c283fed");
            var correlationId2 = new System.Guid("6db9e26d-177c-4cff-b190-f63755e537b1");

            logger.Log(this._timestampOne, Microsoft.Extensions.Logging.LogLevel.Information, 0, "Info message", null,
                (state, ex) => state, correlationId: correlationId1);
            logger.Log(this._timestampOne.AddDays(1), Microsoft.Extensions.Logging.LogLevel.Error, 0, "Error message",
                null, (state, ex) => state, correlationId2);

            provider.IntervalControl.Resume();

            await provider.IntervalControl.Pause;

            var expected1 =
                $"{{\r\n  \"CorrelationId\": \"{correlationId1}\",\r\n  \"EventId\": {{\r\n    \"Id\": 0\r\n  }},\r\n  \"LogLevel\": 2,\r\n  \"LogType\": \"Cat\",\r\n  \"State\": \"Info message\",\r\n  \"Timestamp\": \"2016-05-04T03:02:01+00:00\"\r\n}}";
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(
                expected1,
                System.IO.File.ReadAllText(System.IO.Path.Combine(this.TempPath, "LogFile.20160504.txt")));

            var expected2 = $"{{\r\n  \"CorrelationId\": \"{correlationId2}\",\r\n  \"EventId\": {{\r\n    \"Id\": 0\r\n  }},\r\n  \"LogLevel\": 4,\r\n  \"LogType\": \"Cat\",\r\n  \"State\": \"Error message\",\r\n  \"Timestamp\": \"2016-05-05T03:02:01+00:00\"\r\n}}";
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(
                expected2,
                System.IO.File.ReadAllText(System.IO.Path.Combine(this.TempPath, "LogFile.20160505.txt")));
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public async System.Threading.Tasks.Task WritesToTextFile()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            var provider = new Init.TestFileLoggerProvider(this.TempPath);
            var logger = (BatchLogger.BatchingLogger)provider.CreateLogger("Cat");
            var correlationId = new System.Guid("939221c1-c966-4762-8d6c-77b1e3fe29e3");

            await provider.IntervalControl.Pause;

            logger.Log(timestamp: this._timestampOne, logLevel: Microsoft.Extensions.Logging.LogLevel.Information, eventId: 0, state: "Info message", exception: null, formatter: (state, ex) => state, correlationId: correlationId);
            logger.Log(this._timestampOne.AddHours(1), Microsoft.Extensions.Logging.LogLevel.Error, 0, "Error message",
                null, (state, ex) => state, correlationId: correlationId);

            provider.IntervalControl.Resume();

            await provider.IntervalControl.Pause;

            var expected = $"{{\r\n  \"CorrelationId\": \"{correlationId}\",\r\n  \"EventId\": {{\r\n    \"Id\": 0\r\n  }},\r\n  \"LogLevel\": 2,\r\n  \"LogType\": \"Cat\",\r\n  \"State\": \"Info message\",\r\n  \"Timestamp\": \"2016-05-04T03:02:01+00:00\"\r\n}}{{\r\n  \"CorrelationId\": \"{correlationId}\",\r\n  \"EventId\": {{\r\n    \"Id\": 0\r\n  }},\r\n  \"LogLevel\": 4,\r\n  \"LogType\": \"Cat\",\r\n  \"State\": \"Error message\",\r\n  \"Timestamp\": \"2016-05-04T04:02:01+00:00\"\r\n}}";

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(
                expected,
                System.IO.File.ReadAllText(System.IO.Path.Combine(this.TempPath, "LogFile.20160504.txt")));
        }

        #endregion Public Methods
    }
}
