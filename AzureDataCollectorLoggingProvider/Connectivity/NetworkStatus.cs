namespace AzureDataCollectorLoggingProvider.Connectivity
{
    /// <summary>
    ///  Provides notification of status changes related to Internet-specific network adapters on
    ///  this machine. All other adpaters such as tunneling and loopbacks are ignored. Only connected
    ///  IP adapters are considered.
    /// </summary>
    /// <remarks>
    ///  <i>Implementation Note:</i>
    ///  <para>
    ///   Since we'll likely invoke the IsAvailable property very frequently, that should be very
    ///   efficient. So we wire up handlers for both NetworkAvailabilityChanged and
    ///   NetworkAddressChanged and capture the state in the local isAvailable variable.
    ///  </para>
    ///  <![CDATA[ <see
    ///  cref="https://www.codeproject.com/Articles/64975/Detect-Internet-Network-Availability"/> ]]>
    /// </remarks>
    public static class NetworkStatus
    {
        #region Private Fields

        private static NetworkStatusChangedHandler _handler;
        private static System.Boolean _isAvailable;

        #endregion Private Fields

        //========================================================================================
        // Constructor
        //========================================================================================

        #region Public Properties

        [JetBrains.Annotations.UsedImplicitly]
        public static System.Boolean IsAvailable => NetworkStatus._isAvailable;

        #endregion Public Properties

        #region Public Events

        /// <summary>
        ///  This event is fired when the overall Internet connectivity changes. All non-Internet
        ///  adpaters are ignored. If at least one valid Internet connection is "up" then we consider
        ///  the Internet "available".
        /// </summary>
        [JetBrains.Annotations.UsedImplicitly]
        public static event NetworkStatusChangedHandler AvailabilityChanged
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            add
            {
                if (NetworkStatus._handler == null)
                {
                    System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged +=
                        NetworkStatus.DoNetworkAvailabilityChanged;

                    System.Net.NetworkInformation.NetworkChange.NetworkAddressChanged +=
                        NetworkStatus.DoNetworkAddressChanged;
                }

                NetworkStatus._handler =
                    (NetworkStatusChangedHandler)System.Delegate.Combine(NetworkStatus._handler, value);
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
            remove
            {
                NetworkStatus._handler =
                    (NetworkStatusChangedHandler)System.Delegate.Remove(NetworkStatus._handler, value);

                if (NetworkStatus._handler == null)
                {
                    System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged -=
                        NetworkStatus.DoNetworkAvailabilityChanged;

                    System.Net.NetworkInformation.NetworkChange.NetworkAddressChanged -=
                        NetworkStatus.DoNetworkAddressChanged;
                }
            }
        }

        #endregion Public Events

        #region Public Constructors

        /// <summary>
        ///  Initialize the class by detecting the start condition.
        /// </summary>
        static NetworkStatus() => NetworkStatus._isAvailable = NetworkStatus.IsNetworkAvailable();

        #endregion Public Constructors

        /// <summary>
        ///  Gets a Boolean value indicating the current state of Internet connectivity.
        /// </summary>
        //========================================================================================
        // Properties
        //========================================================================================
        //========================================================================================
        // Methods
        //========================================================================================

        #region Private Methods

        private static void DoNetworkAddressChanged(System.Object sender, System.EventArgs e)
        {
            NetworkStatus.SignalAvailabilityChange(sender);
        }

        private static void DoNetworkAvailabilityChanged(System.Object sender,
                    System.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            NetworkStatus.SignalAvailabilityChange(sender);
        }

        /// <summary>
        ///  Evaluate the online network adapters to determine if at least one of them is capable of
        ///  connecting to the Internet.
        /// </summary>
        /// <returns></returns>
        private static System.Boolean IsNetworkAvailable()
        {
            // only recognizes changes related to Internet adapters
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return false;
            }

            // however, this will include all adapters
            System.Net.NetworkInformation.NetworkInterface[] interfaces =
                System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            foreach (var face in interfaces)
            {
                // filter so we see only Internet adapters
                if (face.OperationalStatus != System.Net.NetworkInformation.OperationalStatus.Up)
                {
                    continue;
                }

                if (face.NetworkInterfaceType == System.Net.NetworkInformation.NetworkInterfaceType.Tunnel ||
                    face.NetworkInterfaceType == System.Net.NetworkInformation.NetworkInterfaceType.Loopback)
                {
                    continue;
                }

                var statistics = face.GetIPv4Statistics();

                // all testing seems to prove that once an interface comes online it has already
                // accrued statistics for both received and sent...

                if (statistics.BytesReceived > 0 && statistics.BytesSent > 0)
                {
                    return true;
                }
            }

            return false;
        }

        private static void SignalAvailabilityChange(System.Object sender)
        {
            var change = NetworkStatus.IsNetworkAvailable();

            if (change == NetworkStatus._isAvailable)
            {
                return;
            }

            NetworkStatus._isAvailable = change;

            if (NetworkStatus._handler != null)
            {
                NetworkStatus._handler(sender, new NetworkStatusChangedArgs(NetworkStatus._isAvailable));
            }
        }

        #endregion Private Methods
    }
}
