namespace AzureDataCollectorLoggingProvider
{
    public class DataCollectorLoggerExtensions
    {
        //#region adapting Microsoft.Extensions.Logging static methods to instance methods
        //#region LogCritical
        // <inheritdoc />
        // <summary>
        // Formats and writes a critical log message.
        // </summary>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogCritical(System.String message, params System.Object[] args)
        //{
        //    this.Logger.LogCritical(message, args);
        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes a critical log message.
        // </summary>
        // <param name="exception">The exception to log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogCritical(System.Exception exception, System.String message, params System.Object[] args)
        //{
        //    this.Logger.LogCritical(exception, message, args);
        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes a critical log message.
        // </summary>
        // <param name="eventId">The event id associated with the log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogCritical(Microsoft.Extensions.Logging.EventId eventId, System.String message,
        //                        params System.Object[]               args)
        //{
        //    this.Logger.LogCritical(eventId, message, args);
        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes a critical log message.
        // </summary>
        // <param name="eventId">The event id associated with the log.</param>
        // <param name="exception">The exception to log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogCritical(Microsoft.Extensions.Logging.EventId eventId, System.Exception       exception,
        //                        System.String                        message, params System.Object[] args)
        //{
        //    this.Logger.LogCritical(eventId, exception, message, args);

        //}

        //#endregion LogCritical

        //#region LogDebug

        // <inheritdoc />
        // <summary>
        // Formats and writes a debug log message.
        // </summary>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogDebug(System.String message, params System.Object[] args)
        //{
        //    this.Logger.LogDebug(message, args);
        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes a debug log message.
        // </summary>
        // <param name="eventId">The event id associated with the log.</param>
        // <param name="exception">The exception to log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogDebug(Microsoft.Extensions.Logging.EventId eventId, System.Exception       exception,
        //                     System.String                        message, params System.Object[] args)
        //{
        //    this.Logger.LogDebug(eventId, exception, message, args);

        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes a debug log message.
        // </summary>
        // <param name="eventId">The event id associated with the log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogDebug(Microsoft.Extensions.Logging.EventId eventId, System.String message,
        //                     params System.Object[]               args)
        //{
        //    this.Logger.LogDebug(eventId, message, args);
        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes a debug log message.
        // </summary>
        // <param name="exception">The exception to log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogDebug(System.Exception exception, System.String message, params System.Object[] args)
        //{
        //    this.Logger.LogDebug(exception, message, args);
        //}

        //#endregion LogDebug

        //#region LogError
        // <inheritdoc />
        // <summary>
        // Formats and writes an error log message.
        // </summary>
        // <param name="exception">The exception to log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogError(System.Exception exception, System.String message, params System.Object[] args)
        //{
        //    this.Logger.LogError(exception, message, args);
        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes an error log message.
        // </summary>
        // <param name="eventId">The event id associated with the log.</param>
        // <param name="exception">The exception to log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogError(Microsoft.Extensions.Logging.EventId eventId, System.Exception       exception,
        //                     System.String                        message, params System.Object[] args)
        //{
        //    this.Logger.LogError(eventId, exception, message, args);
        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes an error log message.
        // </summary>
        // <param name="eventId">The event id associated with the log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogError(Microsoft.Extensions.Logging.EventId eventId, System.String message,
        //                     params System.Object[]               args)
        //{
        //    this.Logger.LogError(eventId, message, args);
        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes an error log message.
        // </summary>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogError(System.String message, params System.Object[] args)
        //{
        //    this.Logger.LogError(message, args);
        //}

        //#endregion LogError

        //#region LogInformation
        // <inheritdoc />
        // <summary>
        // Formats and writes an informational log message.
        // </summary>
        // <param name="eventId">The event id associated with the log.</param>
        // <param name="exception">The exception to log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogInformation(Microsoft.Extensions.Logging.EventId eventId, System.Exception       exception,
        //                           System.String                        message, params System.Object[] args)
        //{
        //    this.Logger.LogInformation(eventId, exception, message, args);
        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes an informational log message.
        // </summary>
        // <param name="eventId">The event id associated with the log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogInformation(Microsoft.Extensions.Logging.EventId eventId, System.String message,
        //                           params System.Object[]               args)
        //{
        //    this.Logger.LogInformation(eventId, message, args);
        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes an informational log message.
        // </summary>
        // <param name="exception">The exception to log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogInformation(System.Exception exception, System.String message, params System.Object[] args)
        //{
        //    this.Logger.LogInformation(exception, message, args);
        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes an informational log message.
        // </summary>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogInformation(System.String message, params System.Object[] args)
        //{
        //    this.Logger.LogInformation(message, args);
        //}
        //#endregion LogInformation

        //#region LogTrace
        // <inheritdoc />
        // <summary>
        // Formats and writes an trace log message.
        // </summary>
        // <param name="exception">The exception to log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogTrace(System.Exception exception, System.String message, params System.Object[] args)
        //{
        //    this.Logger.LogTrace(exception, message, args);
        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes an trace log message.
        // </summary>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogTrace(System.String message, params System.Object[] args)
        //{
        //    this.Logger.LogTrace(message, args);
        //}

        // <inheritdoc />
        //  <summary>
        //Formats and writes an trace log message.
        // </summary>
        // <param name="eventId">
        //The event id associated with the log.
        // </param>
        // <param name="exception">
        //The exception to log.
        // </param>
        // <param name="message">
        //Format string of the log message.
        // </param>
        // <param name="args">
        //An object array that contains zero or more objects to format.
        // </param>
        //public void LogTrace(Microsoft.Extensions.Logging.EventId eventId, System.Exception exception,
        //                     System.String message, params System.Object[] args)
        //{
        //    this.Logger.LogTrace(eventId, exception, message, args);
        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes an trace log message.
        // </summary>
        // <param name="eventId">The event id associated with the log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogTrace(Microsoft.Extensions.Logging.EventId eventId, System.String message,
        //                     params System.Object[]               args)
        //{
        //    this.Logger.LogTrace(eventId, message, args);
        //}

        //#endregion LogTrace

        //#region LogWarning
        // <inheritdoc />
        // <summary>
        // Formats and writes an warning log message.
        // </summary>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogWarning(System.String message, params System.Object[] args)
        //{
        //    this.Logger.LogWarning(message, args);
        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes an warning log message.
        // </summary>
        // <param name="eventId">The event id associated with the log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogWarning(Microsoft.Extensions.Logging.EventId eventId, System.String message,
        //                       params System.Object[]               args)
        //{
        //    this.Logger.LogWarning(eventId, message, args);
        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes an warning log message.
        // </summary>
        // <param name="exception">The exception to log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogWarning(System.Exception exception, System.String message, params System.Object[] args)
        //{
        //    this.Logger.LogWarning(exception, message, args);
        //}

        // <inheritdoc />
        // <summary>
        // Formats and writes an warning log message.
        // </summary>
        // <param name="eventId">The event id associated with the log.</param>
        // <param name="exception">The exception to log.</param>
        // <param name="message">Format string of the log message.</param>
        // <param name="args">An object array that contains zero or more objects to format.</param>
        //public void LogWarning(Microsoft.Extensions.Logging.EventId eventId, System.Exception       exception,
        //                       System.String                        message, params System.Object[] args)
        //{
        //    this.Logger.LogWarning(eventId, exception, message, args);
        //}
        //#endregion LogWarning
        //#endregion adapting Microsoft.Extensions.Logging static methods to instance methods
    }
}
