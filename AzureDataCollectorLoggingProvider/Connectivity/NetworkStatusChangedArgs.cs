// ReSharper disable MemberCanBePrivate.Global
namespace AzureDataCollectorLoggingProvider.Connectivity
{
    /// <inheritdoc />
    /// <summary>
    ///  Describes the overall network connectivity of the machine.
    /// </summary>
    /// <remarks>
    ///  <![CDATA[ <see
    ///  cref="https://www.codeproject.com/Articles/64975/Detect-Internet-Network-Availability"/> ]]>
    /// </remarks>
    public sealed class NetworkStatusChangedArgs : System.EventArgs
    {
        #region Public Properties

        /// <summary>
        ///  Gets a Boolean value indicating the current state of Internet connectivity.
        /// </summary>
        public System.Boolean IsAvailable { [JetBrains.Annotations.UsedImplicitly] get; }

        #endregion Public Properties

        #region Public Constructors

        /// <inheritdoc />
        /// <summary>
        ///  Instantiate a new instance with the given availability.
        /// </summary>
        /// <param name="isAvailable">
        ///</param>
        public NetworkStatusChangedArgs(System.Boolean isAvailable) => this.IsAvailable = isAvailable;

        #endregion Public Constructors
    }
}
