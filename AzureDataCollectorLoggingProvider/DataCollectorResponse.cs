namespace AzureDataCollectorLoggingProvider
{
    [JetBrains.Annotations.UsedImplicitly]
    public class DataCollectorResponse : Interfaces.IDataCollectorResonse
    {
        #region Public Properties

        public System.Net.Http.HttpContent Content { get; set; }
        public System.String DateString { get; set; }
        public System.Exception Exception { get; set; }
        public System.Net.Http.HttpResponseMessage Response { get; set; }
        public System.String Result { get; set; }
        public System.String State { get; set; }
        public System.String Url { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///  Returns a json formatted string that represents the current object.
        /// </summary>
        /// <returns>A json formatted string that represents the current object.</returns>
        [JetBrains.Annotations.UsedImplicitly]
        public System.String ToJson() =>
            Newtonsoft.Json.JsonConvert.SerializeObject(this,
                Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings
                {
                    TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All,
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });

        #endregion Public Methods
    }
}
