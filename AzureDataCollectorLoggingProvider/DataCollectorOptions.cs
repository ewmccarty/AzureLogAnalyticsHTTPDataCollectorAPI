/*
 * Derived from https://docs.microsoft.com/en-us/azure/log-analytics/log-analytics-data-collector-api
 * Derived from NetEscapades.Extensions.Logging.RollingFile
 * Erik w. McCarty 2018-03026
 */

// ReSharper disable ClassCanBeSealed.Global
namespace AzureDataCollectorLoggingProvider
{
    /// <summary>
    ///  Options for Azure Data Collector and fail-back File logging.
    /// </summary>
    [JetBrains.Annotations.UsedImplicitly]
    public class DataCollectorOptions : RollingFileLogger.FileLoggerOptions, Interfaces.IDataCollectorOptions
    {
        #region Private Fields

        private System.TimeSpan _webTimeout = System.TimeSpan.FromSeconds(10);

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        ///  Update customerId to your Log Analytics workspace ID From https://portal.azure.com/#@cloudwalkertx.onmicrosoft.com/resource/subscriptions/1d77d23f-c627-409b-8cba-fbd9d6370dcf/resourceGroups/Log/providers/Microsoft.OperationalInsights/workspaces/WEIOMSWorkspace/Overview
        /// </summary>
        [JetBrains.Annotations.UsedImplicitly]
        public System.String CustomerID { get; set; }

        /// <summary>
        ///  For sharedKey, use either the primary or the secondary Connected Sources client
        ///  authentication key https://portal.azure.com/#@{domain}.onmicrosoft.com/resource/subscriptions/{subscriptionId}/resourceGroups/Log/providers/Microsoft.OperationalInsights/workspaces/{WorkspaceName}/advancedSettings
        ///  - Home &gt; Log &gt; WEIOMSWorkspace &gt; Advanced settings &gt; Connected Sources &gt;
        ///    Windows Servers
        /// </summary>
        [JetBrains.Annotations.UsedImplicitly]
        public System.String SharedKey { get; set; }

        /// <summary>
        ///  Azure Data Collector Logger Provider Web Timeout before logging backup queue to file
        /// </summary>
        [JetBrains.Annotations.UsedImplicitly]
        public System.TimeSpan? WebTimeOut
        {
            get => this._webTimeout;
            set
            {
                if (value == System.TimeSpan.Zero || value == null)
                {
                    throw new System.ArgumentException("Azure Data Collector Logger Provider Timeout is zero. Pick a more reasonable value.", nameof(value));
                }

                if (value >= System.TimeSpan.FromMinutes(5))
                {
                    throw new System.ArgumentException("Azure Data Collector Logger Provider Timeout exceeds 5 minutes. Pick a more reasonable value.", nameof(value));
                }

                this._webTimeout = (System.TimeSpan)value;
            }
        }

        #endregion Public Properties
    }
}
