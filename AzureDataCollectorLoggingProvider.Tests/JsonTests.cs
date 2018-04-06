namespace AzureDataCollectorLoggingProvider.Tests
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class JsonTests : TestContextBase
    {
        #region Public Methods

        private const System.String Json = @"[{""DemoField1"":""DemoValue1"",""DemoField2"":""DemoValue2""},{""DemoField3"":""DemoValue3"",""DemoField4"":""DemoValue4""}]";
        private const System.String JsonFail = @"""DemoField1"":""DemoValue1"",""DemoField2"":""DemoValue2""},{""DemoField3"":""DemoValue3"",""DemoField4"":""DemoValue4""}]";

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void JContainerTest()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            Newtonsoft.Json.Linq.JToken.Parse(JsonTests.Json);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(true);
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void JContainerTestFail()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Newtonsoft.Json.JsonReaderException>(
                () => Newtonsoft.Json.Linq.JToken.Parse(JsonTests.JsonFail));
        }

        #endregion Public Methods
    }
}
