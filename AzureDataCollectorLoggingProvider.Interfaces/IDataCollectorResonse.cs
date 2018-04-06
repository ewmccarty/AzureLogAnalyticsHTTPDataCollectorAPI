namespace AzureDataCollectorLoggingProvider.Interfaces
{
    public interface IDataCollectorResonse
    {
        #region Public Properties

        System.Net.Http.HttpContent Content { get; set; }
        System.String DateString { get; set; }
        System.Exception Exception { get; set; }
        System.Net.Http.HttpResponseMessage Response { get; set; }
        System.String Result { get; set; }
        System.String State { get; set; }
        System.String Url { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///  Returns a json formatted string that represents the current object.
        /// </summary>
        /// <returns>A json formatted string that represents the current object.</returns>
        System.String ToJson();

        /// <summary>
        ///  Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        System.String ToString();

        #endregion Public Methods
    }
}
