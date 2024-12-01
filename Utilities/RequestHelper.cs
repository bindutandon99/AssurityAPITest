
using RestSharp;        

namespace AssurityAPITests.Utilities
{


    /// <summary>
    /// A helper class for creating and executing HTTP requests using RestSharp.
    /// </summary>
    internal class RequestHelper
    {

        public RestClient CreateRestClient(string baseUrl)
        {
          RestClient client = new RestClient(baseUrl);
            return client;  
        }

        
        public RestResponse ExecuteRequest(RestClient client, RestRequest request)
        {
            return client.Execute(request);
        }

    }
}
