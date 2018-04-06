namespace AzureDataCollectorLoggingProvider.Tests
{
    [JetBrains.Annotations.UsedImplicitly]
    public class TestContextBase
    {
        #region TestContext

        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        /// <remarks>
        /// <![CDATA[
        /// <see cref="https://social.msdn.microsoft.com/Forums/en-US/51caf5f5-bd62-4b1e-bf48-059498744125/how-to-output-text-message-from-a-test-method-a-function-with-testmethod-attribute-?forum=vststest"/>
        /// ]]>
        /// The test context is a property on your test class. It's a well known property, rather than something you mark up.
        ///     a) It must be Property Getter/Setter.
        ///     b) It must be called "TestContext"
        ///     c) and it must be of Type "TestContext"
        ///
        /// eg:
        /// </remarks>
// ReSharper disable once RedundantNameQualifier
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;

        // ReSharper disable once ConvertToAutoProperty ReSharper disable once UnusedMember.Global
        // ReSharper disable once RedundantNameQualifier ReSharper disable once MemberCanBePrivate.Global
        public Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext
        {
            get => this._testContext;
            set => this._testContext = value;
        }

        #endregion TestContext
    }
}
