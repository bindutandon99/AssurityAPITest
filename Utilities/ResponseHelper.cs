using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AssurityAPITests.Utilities
{
    internal class ResponseHelper
    {


        /// <summary>
        /// Gets value from the json response  as JToken
        /// This method is useful in case where the deserialization is not preferred
        /// </summary>
        /// <param name="responseContent"></param>
        /// <param name="jsonPath"></param>
        /// <returns>JToken representing given Json Path or null if not found</returns>
        public JToken? GetValueFromJsonResponse(string responseContent, string jsonPath)
        {
            try
            {
                // Parse the JSON response into a JObject
                var responseObject = JObject.Parse(responseContent);

                // Use SelectToken to query the JSON with the specified JSONPath
                var token = responseObject.SelectToken(jsonPath);

                // If the token is null, throw exception
                if (token == null)
                {
                    throw new InvalidOperationException($"The specified JSONPath '{jsonPath}' was not found in the response.");
                }

                return token;
            }
            catch (JsonReaderException ex)
            {
                throw new InvalidOperationException("Failed to parse JSON response. Ensure the response content is valid JSON.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while querying the JSON response.", ex);
            }
        }


        
        /// <summary>
        /// Deserialize the JSON response in object of specific type
        /// </summary>
        /// <param name="responseContent"></param>
        /// <returns>object of type T populated with data from JSON response</returns>
        public T DeserializeResponseToObject<T>(string responseContent)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<T>(responseContent);

                //throw exception if deserialization of object resulting in null
                if (result == null)
                {
                    throw new InvalidOperationException("Deserialization returned null. Please make sure the response content is valid ");
                }

                return result;
            }

            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to deserialize the response content to the specified object type", ex);
            }
        }
    }
    }


