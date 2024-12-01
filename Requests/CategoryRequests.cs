using AssurityAPITests.Log;
using AssurityAPITests.Models;
using AssurityAPITests.Utilities;
using RestSharp;


namespace AssurityAPITests.Builders
{

      /// <summary>
     /// Represents a class that handles the construction and execution of API requests
    /// It retrieves and processes category and promotion related information from API response.
    /// </summary>
    internal class CategoryRequests :ApiEndPointBuilder
    {

       
        private readonly RequestContextBuilder _requestContextBuilder;
        private CategoryDetails _categoryDetails;
        private Promotion _promotionlist;
        private ResponseHelper _responseHelper;
      
        public CategoryRequests()
        {
            _requestContextBuilder = new RequestContextBuilder();
            _responseHelper = new ResponseHelper();
            _categoryDetails = new CategoryDetails();
            _promotionlist = new Promotion();
        }

        
         /// <summary>
        /// Sends a GET request to retrieve category data based on the provided ID and catalog.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="catalogue"></param>
        /// <returns>A RestResponse object containing the response from the GET request.</returns>
        public RestResponse getCategories(string id, string catalogue)
        {
            // Sends a GET request to the categories endpoint using the constructed URI for the given ID and catalogue.
            // CreateRestRequest method can accept optional headers  as well
           
            return _requestContextBuilder.CreateRestRequest(GetCategoriesUri(id, catalogue), Method.Get);
        }


        /// <summary>
        /// Deserializes the JSON response content into a CategoryDetails object and returns it.
        /// </summary>
        /// <param name="responseContent">The JSON response content as a string.</param>
        /// <returns>Deserialized CategoryDetails object.</returns>
        public CategoryDetails getCategoryDetails(string responseContent) 
        {
            _categoryDetails = _responseHelper.DeserializeResponseToObject<CategoryDetails>(responseContent);
            return _categoryDetails;
        }


        /// <summary>
        /// Retrieves the description text for a given promotion name from a list of promotions.
        /// </summary>
        /// <param name="promotionsList"></param>
        /// <param name="promotionName"></param>
        /// <returns>The description of the promotion if found(blank string in case description is null); otherwise, a message indicating the promotion was not found.</returns>
        public string getTextForPromotionName(List<Promotion> promotionsList, string promotionName)
        {



            // Find the promotion with the given name
            Promotion? promotion = promotionsList.FirstOrDefault(p => string.Equals(p.Name, promotionName, StringComparison.OrdinalIgnoreCase));

            if (promotion != null)
            {
                Logger.log.Information($"For Promotion : {promotionName} , The description text is Description: {promotion.Description}");
                // If Description is null then return blank string
                return promotion.Description ?? "";
            }
            else
            {
                // In case promotion is not found, return appropriate message 
                Logger.log.Warning($"Promotion with the name '{promotionName}' not found.");
                return $"Promotion with the name '{promotionName}' not found.";
            }


        }

    }




    }

