//************************************************************************************************
// Copyright © 2010 Steven M. Cohn. All Rights Reserved.
//
//************************************************************************************************

namespace AzureDataCollectorLoggingProvider.Tests
{
    /// <summary>
    ///  Demonstrate the use of the NetworkStatus class.
    /// </summary>
    /// <remarks>TODO I'm not satisfied that this is actually doing it's job</remarks>
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public sealed class ConnectivityTests : TestContextBase
    {
        #region Public Methods

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void ConnectivityMainTest()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");

            this.ReportAvailability();

            Connectivity.NetworkStatus.AvailabilityChanged +=
                this.DoAvailabilityChanged;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(Connectivity.NetworkStatus.IsAvailable);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///  Event handler used to capture availability changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoAvailabilityChanged(System.Object sender, Connectivity.NetworkStatusChangedArgs e)
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");

            this.ReportAvailability();
        }

        /// <summary>
        ///  Report the current network availability.
        /// </summary>
        private void ReportAvailability()
        {
            this.TestContext.WriteLine($"{{ {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name} }}");

            this.TestContext.WriteLine(Connectivity.NetworkStatus.IsAvailable
                ? "... Network is available"
                : "... Network is not available");
        }

        #endregion Private Methods
    }
}
