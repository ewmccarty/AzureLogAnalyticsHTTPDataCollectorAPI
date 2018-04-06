namespace AzureDataCollectorLoggingProvider.Generic
{
    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]
    public struct LogMessages<TState> : Interfaces.Generic.ILogMessages<TState>
    {
        #region Private Properties

        [Newtonsoft.Json.JsonProperty]
        public System.Collections.Generic.IEnumerable<Interfaces.Generic.ILogMessage<TState>> Messages { get; set; }

        #endregion Private Properties

        #region Public Methods

        /// <inheritdoc />
        /// <summary>
        ///  Returns a json formatter string that represents the current object.
        /// </summary>
        /// <returns>A json formatter string that represents the current object.</returns>
        [JetBrains.Annotations.UsedImplicitly]
        public System.String ToJson() =>
            Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented,
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None,
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                });

        #endregion Public Methods
    }
}
