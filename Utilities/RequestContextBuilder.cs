using AssurityAPITests.Log;
using RestSharp;

namespace AssurityAPITests.Utilities
{
    internal class RequestContextBuilder : RequestHelper
    {
        string baseUrl;
        
        public RequestContextBuilder() {
            baseUrl = AppConfig.GetConfigValue("BaseUrl");
        }


        /// <summary>
        /// Sends an API request to the specified resource using the given HTTP method.
        /// </summary>
        /// <param name="resource">The endpoint or resource path for the API request.</param>
        /// <param name="method">The HTTP method (e.g., GET, POST, PUT) used for the request.</param>
        /// <param name="headers">Optional dictionary of headers to include in the request (e.g., Authorization, Content-Type).</param>
        /// <param name="body">Optional body parameter for POST request</param>
        /// <returns>An instance of RestResponse class containing the response data.</returns>
        public RestResponse CreateRestRequest(string resource, Method method, Dictionary<string, string>? headers = null, object? body = default)
        {
           
           var request = new RestRequest(resource, method);

            // Add headers if provided
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            if (method == Method.Post && body != null)
            {
                request.AddJsonBody(body); // Serializes the body object to JSON
            }

            Logger.log.Information($"The request is sent with {baseUrl+resource} and method is {method} ");
           
           return ExecuteRequest(CreateRestClient(baseUrl),request);
        
        }
   }
}
