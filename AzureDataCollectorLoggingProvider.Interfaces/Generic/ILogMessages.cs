namespace AzureDataCollectorLoggingProvider.Interfaces.Generic
{
    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
    public interface ILogMessages<TState>
    {
        #region Public Properties

        //System.Collections.Generic.IEnumerable<System.Linq.IGrouping<System.String, ILogMessage<TState>>> LogPartitions { get; }
        [Newtonsoft.Json.JsonProperty]
        System.Collections.Generic.IEnumerable<ILogMessage<TState>> Messages { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///  Returns a json formatted string that represents the current object.
        /// </summary>
        /// <returns>A json formatted string that represents the current object.</returns>
        [JetBrains.Annotations.UsedImplicitly]
        System.String ToJson();

        #endregion Public Methods
    }
}
