namespace AzureDataCollectorLoggingProvider.Connectivity
{
    /// <summary>
    ///  Define the method signature for network status changes.
    /// </summary>
    /// <param name="sender">///</param>
    /// <param name="e">///</param>
    /// <remarks>
    ///  <![CDATA[ <see
    ///  cref="https://www.codeproject.com/Articles/64975/Detect-Internet-Network-Availability"/> ]]>
    /// </remarks>
    [JetBrains.Annotations.UsedImplicitly]
    public delegate void NetworkStatusChangedHandler(System.Object sender, NetworkStatusChangedArgs e);
}
