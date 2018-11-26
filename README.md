# AzureLogAnalyticsHTTPDataCollectorAPI
.NET Standard 2.1 Logger Provider for the Azure HTTP Data Collector API (public preview) in Log Analytics 

https://docs.microsoft.com/en-us/azure/log-analytics/log-analytics-data-collector-api
(Much of the content from the above page is copied herein as the author may make changes)

An (see cref="T:Microsoft.Extensions.Logging.ILoggerProvider" ) that writes logs to the
Azure Analytics HTTP Data Collector API with file system fail-back

	A significant challeng in this design is managing to the potentially disconnected 
	environment without losing data. This logging provider will fail-back to a local file 
	system logger when disconnected, and then purge the local log files when connectivity
	returns. A maximum number of local log files and their maximum size can be set in 
	the options, and any loss of data will be reported when connectivity retsumes.
	Initially I envisioned this derived from NetEscapades.Extensions.Logging.RollingFile.FileLogger
	to handle the fail-back, but after some trial and error concluded that the LogMessage Type
	desired for the Azure Data Collector Provider requires more structure than that required
	by the FileLogger. Complicated largely by the definition of LogMessage as a Struct, so I've 
	blatenly copied and modified the FileLogger as seems appropriate.
	
https://docs.microsoft.com/en-us/azure/log-analytics/log-analytics-data-collector-api 
This article shows you how to use the HTTP Data Collector API to send data to Log Analytics from
a REST API client. It describes how to format data collected by your script or application,
include it in a request, and have that request authorized by Log Analytics. Examples are
provided for PowerShell, C#, and Python.
    
Concepts
    
You can use the HTTP Data Collector API to send data to Log Analytics from any client that
can call a REST API. This might be a runbook in Azure Automation that collects management
data from Azure or another cloud, or it might be an alternate management system that uses
Log Analytics to consolidate and analyze data.
    
All data in the Log Analytics repository is stored as a record with a particular record
type. You format your data to send to the HTTP Data Collector API as multiple records in
JSON. When you submit the data, an individual record is created in the repository for each
record in the request payload.
    
Create a request
    
To use the HTTP Data Collector API, you create a POST request that includes the data to send
in JavaScript Object Notation (JSON). The next three tables list the attributes that are
required for each request. We describe each attribute in more detail later in the article.
    
Request URI
- Attribute Property
- Method POST
- URI https://{CustomerId}.ods.opinsights.azure.com/api/logs?api-version=2016-04-01
- Content type application/json
    
Request URI parameters
    
- Parameter Description
- CustomerID The unique identifier for the Log Analytics workspace.
- Resource The API resource name: /api/logs.
- API Version The version of the API to use with this request. Currently, it's 2016-04-01.
    
Request headers
    
Header Description
- Authorization The authorization signature. Later in the article, you can read about how to
  create an HMAC-SHA256 header.
- Log-Type Specify the record type of the data that is being submitted. Currently, the log
  type supports only alpha characters. It does not support numerics or special characters.
- x-ms-date The date that the request was processed, in RFC 1123 format.
- time-generated-field The name of a field in the data that contains the timestamp of the
  data item. If you specify a field then its contents are used for TimeGenerated. If this
  field isn’t specified, the default for TimeGenerated is the time that the message is
  ingested. The contents of the message field should follow the ISO 8601 format YYYY-MM-DDThh:mm:ssZ.
    
Authorization
    
Any request to the Log Analytics HTTP Data Collector API must include an authorization
header. To authenticate a request, you must sign the request with either the primary or the
secondary key for the workspace that is making the request. Then, pass that signature as
part of the request.
    
Here's the format for the authorization header:
    
Authorization: SharedKey (WorkspaceID):(Signature)
    
WorkspaceID is the unique identifier for the Log Analytics workspace. Signature is a
Hash-based Message Authentication Code (HMAC) that is constructed from the request and then
computed by using the SHA256 algorithm. Then, you encode it by using Base64 encoding.
    
Use this format to encode the SharedKey signature string:
    
StringToSign = VERB + "\n" + Content-Length + "\n" + Content-Type + "\n" + x-ms-date + "\n"
+ "/api/logs";
    
Here's an example of a signature string:
    
POST\n1024\napplication/json\nx-ms-date:Mon, 04 Apr 2016 08:00:00 GMT\n/api/logs
    
When you have the signature string, encode it by using the HMAC-SHA256 algorithm on the
UTF-8-encoded string, and then encode the result as Base64. Use this format:
    
Signature=Base64(HMAC-SHA256(UTF8(StringToSign)))
    
The samples in the next sections have sample code to help you create an authorization header.
    
Request body The body of the message must be in JSON. It must include one or more records
with the property name and value pairs in this format:
    
- {
- "property1": "value1",
- "property 2": "value2",
- "property 3": "value3",
- "property 4": "value4"
- }
    
You can batch multiple records together in a single request by using the following format.
All the records must be the same record type.
    
- {
- "property1": "value1",
- "property 2": "value2",
-  " property 3": "value3",
-  " property 4": "value4"
- },
- {
- "property1": "value1",
- "property 2": "value2",
- "property 3": "value3",
- "property 4": "value4"
- }
    
Record type and properties
    
You define a custom record type when you submit data through the Log Analytics HTTP Data
Collector API. Currently, you can't write data to existing record types that were created by
other data types and solutions. Log Analytics reads the incoming data and then creates
properties that match the data types of the values that you enter.
    
Each request to the Log Analytics API must include a Log-Type header with the name for the
record type. The suffix _CL is automatically appended to the name you enter to distinguish
it from other log types as a custom log. For example, if you enter the name MyNewRecordType,
Log Analytics creates a record with the type MyNewRecordType_CL. This helps ensure that
there are no conflicts between user-created type names and those shipped in current or
future Microsoft solutions.
    
To identify a property's data type, Log Analytics adds a suffix to the property name. If a
property contains a null value, the property is not included in that record. This table
lists the property data type and corresponding suffix:
    
- Property data type Suffix
- String _s
- Boolean _b
- Double _d
- Date/time _t
- GUID _g
    
The data type that Log Analytics uses for each property depends on whether the record type
for the new record already exists.
    
- If the record type does not exist, Log Analytics creates a new one. Log Analytics uses the
  JSON type inference to determine the data type for each property for the new record.
- If the record type does exist, Log Analytics attempts to create a new record based on
  existing properties. If the data type for a property in the new record doesn’t match and
  can’t be converted to the existing type, or if the record includes a property that doesn’t
  exist, Log Analytics creates a new property that has the relevant suffix.
    
For example, this submission entry would create a record with three properties, number_d,
boolean_b, and string_s:
    
    
- {
- "number": 32,
- "boolean": true,
- "string": "MyText"
- }
    
6/2/2016 10:35:06.912 AM | MyRecordType_CL
- ... TimeGenerated : 6/2/2016 10:35:06.912 AM
- ... number_d : 32
- ... string_s : MyText
- ... boolean_b : true
- ... SourceSystem : RestAPI
    
If you then submitted this next entry, with all values formatted as strings, the properties
would not change. These values can be converted to existing data types:
    
    
- {
- "number": "32",
- "boolean": "true",
- "string": "MyText"
- }
    
6/2/2016 10:48:28.997 AM | MyRecordType_CL
- ... TimeGenerated : 6/2/2016 10:48:28.997 AM
- ... number_d : 32
- ... string_s : MyText
- ... boolean_b : true
- ... SourceSystem : RestAPI
    
But, if you then made this next submission, Log Analytics would create the new properties
boolean_d and string_d. These values can't be converted:
    
    
- {
- "number": 33,
- "boolean": 0,
- "string": 27
- }
    
6/2/2016 12:27:54.049 AM | MyRecordType_CL
- ... TimeGenerated : 6/2/2016 12:27:54.049 AM
- ... number_d : 33
- ... boolean_d : 0
- ... string_d : 27
- ... SourceSystem : RestAPI
    
If you then submitted the following entry, before the record type was created, Log Analytics
would create a record with three properties, number_s, boolean_s, and string_s. In this
entry, each of the initial values is formatted as a string:
    
    
- {
- "number": "32",
- "boolean": "true",
- "string": "MyText"
- }
    
6/2/2016 11:35:13.426 AM | MyRecordType_CL
- ... TimeGenerated : 6/2/2016 11:35:13.426 AM
- ... number_s : 32
- ... string_s : MyText
- ... boolean_s : true
- ... SourceSystem : RestAPI
    
Data limits
    
There are some constraints around the data posted to the Log Analytics Data collection API.
    
- Maximum of 30 MB per post to Log Analytics Data Collector API. This is a size limit for a
  single post. If the data from a single post that exceeds 30 MB, you should split the data
  up to smaller sized chunks and send them concurrently.
- Maximum of 32 KB limit for field values. If the field value is greater than 32 KB, the
  data will be truncated.
- Recommended maximum number of fields for a given type is 50. This is a practical limit
  from a usability and search experience perspective. Return codes The HTTP status code 200
  means that the request has been received for processing. This indicates that the operation
  completed successfully.
    
This table lists the complete set of status codes that the service might return:
    
Code Status Error code Description
- 200 OK The request was successfully accepted.
- 400 Bad request InactiveCustomer The workspace has been closed.
- 400 Bad request InvalidApiVersion The API version that you specified was not recognized by
  the service.
- 400 Bad request InvalidCustomerId The workspace ID specified is invalid.
- 400 Bad request InvalidDataFormat Invalid JSON was submitted. The response body might
  contain more information about how to resolve the error.
- 400 Bad request InvalidLogType The log type specified contained special characters or numerics.
- 400 Bad request MissingApiVersion The API version wasn’t specified.
- 400 Bad request MissingContentType The content type wasn’t specified.
- 400 Bad request MissingLogType The required value log type wasn’t specified.
- 400 Bad request UnsupportedContentType The content type was not set to application/json.
- 403 Forbidden InvalidAuthorization The service failed to authenticate the request. Verify
  that the workspace ID and connection key are valid.
- 404 Not Found Either the URL provided is incorrect, or the request is too large.
- 429 Too Many Requests The service is experiencing a high volume of data from your account.
  Please retry the request later.
- 500 Internal Server Error UnspecifiedError The service encountered an internal error.
  Please retry the request.
- 503 Service Unavailable ServiceUnavailable The service currently is unavailable to receive
  requests. Please retry your request.
    
Query data To query data submitted by the Log Analytics HTTP Data Collector API, search for
records with Type that is equal to the LogType value that you specified, appended with _CL.
For example, if you used MyCustomLog, then you'd return all records with Type=MyCustomLog_CL.
    
Note
    
If your workspace has been upgraded to the new Log Analytics query language, then the above
query would change to the following.
    
MyCustomLog_CL
    
Sample requests
    
In the next sections, you'll find samples of how to submit data to the Log Analytics HTTP
Data Collector API by using different programming languages.
    
For each sample, do these steps to set the variables for the authorization header:
    
- In the Azure portal, locate your Log Analytics workspace.
- Select Advanced Settings and then Connected Sources.
- To the right of Workspace ID, select the copy icon, and then paste the ID as the value of
  the Customer ID variable.
- To the right of Primary Key, select the copy icon, and then paste the ID as the value of
  the Shared Key variable.
- Alternatively, you can change the variables for the log type and JSON data.
    
Next steps
    
Use the Log Search API to retrieve data from the Log Analytics repository. https://docs.microsoft.com/en-us/azure/log-analytics/log-analytics-log-search-api
