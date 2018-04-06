namespace AzureDataCollectorLoggingProvider
{
    public static class Common
    {
        #region Public Methods

        /// <summary>
        ///  Removes all special characters, keeping alphabetical letters and numbers.
        /// </summary>
        public static string RemoveAllSpecialCharacters(this string originalString) => System.Text.RegularExpressions.Regex.Replace(originalString, "[^0-9A-Za-z]+", string.Empty);

        /// <summary>
        ///  Converts DateTime to Iso8601 format string
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        /// <![CDATA[ <see
        /// cref="https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#Sortable"/> ]]>
        [JetBrains.Annotations.NotNull]
        public static System.String ToIso8601(this System.DateTime dateTime) => $"{dateTime:O}";

        /// <summary>
        ///  Converts DateTimeOffset to Iso8601 format string
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        /// <![CDATA[ <see
        /// cref="https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#Sortable"/> ]]>
        [JetBrains.Annotations.NotNull]
        public static System.String ToIso8601(this System.DateTimeOffset dateTime) => $"{dateTime:O}";

        /// <summary>
        ///  Returns a json formatted string that represents the current object.
        /// </summary>
        /// <returns>A json formatted string that represents the current object.</returns>
        [JetBrains.Annotations.UsedImplicitly]
        public static System.String ToJson(this System.Object obj) =>
            Newtonsoft.Json.JsonConvert.SerializeObject(obj,
                Newtonsoft.Json.Formatting.Indented,
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None,
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                });

        #endregion Public Methods
    }
}
