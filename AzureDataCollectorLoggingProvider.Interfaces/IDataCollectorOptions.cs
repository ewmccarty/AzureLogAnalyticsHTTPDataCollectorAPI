namespace AzureDataCollectorLoggingProvider.Interfaces
{
    public interface IDataCollectorOptions : RollingFileLogger.IFileLoggerOptions
    {
        #region Public Properties

        /// <summary>
        ///  Update customerId to your Log Analytics workspace ID From https://portal.azure.com/#@cloudwalkertx.onmicrosoft.com/resource/subscriptions/1d77d23f-c627-409b-8cba-fbd9d6370dcf/resourceGroups/Log/providers/Microsoft.OperationalInsights/workspaces/WEIOMSWorkspace/Overview
        /// </summary>
        System.String CustomerID { get; set; }

        /// <summary>
        ///  For sharedKey, use either the primary or the secondary Connected Sources client
        ///  authentication key https://portal.azure.com/#@{domain}.onmicrosoft.com/resource/subscriptions/{subscriptionId}/resourceGroups/Log/providers/Microsoft.OperationalInsights/workspaces/{WorkspaceName}/advancedSettings
        ///  - Home &gt; Log &gt; WEIOMSWorkspace &gt; Advanced settings &gt; Connected Sources &gt;
        ///  Windows Servers
        /// </summary>
        System.String SharedKey { get; set; }

        #endregion Public Properties
    }
}
