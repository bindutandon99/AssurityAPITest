using AssurityAPITests.Builders;
using AssurityAPITests.Log;
using AssurityAPITests.Models;
using NUnit.Framework;
using RestSharp;

namespace AssurityAPITests.StepDefinitions
{
    [Binding]
    public class CategoriesStepDefinitions
    {
        private RestResponse _restResponse;
        private CategoryRequests _categoryRequests;
        private CategoryDetails _categoryDetails;
        

        CategoriesStepDefinitions()
        {

            _categoryRequests = new CategoryRequests();
            _categoryDetails = new CategoryDetails();
            _restResponse = new RestResponse();
            
        }



        [When(@"The user sends API request with category ""([^""]*)"" and   catalogue ""([^""]*)""")]
        public void WhenTheUserSendsAPIRequestWithCategoryAndCatalogue(string id, string catalog)
        {

            // Calls the `getCategories` method from the `_categoryRequests` instance to send a request
            // and retrieve the categories in RestResponse Object  
            _restResponse = _categoryRequests.getCategories(id, catalog);
            
            if (_restResponse == null || _restResponse.Content == null)
            {
                // Handle null case
                throw new InvalidOperationException("The response from getCategories is null.");
            }
        }

        [Then(@"status code should be (.*)")]
        public void ThenStatusCodeShouldBe(int statusCode)
        {
            Logger.log.Information($"API response status code is {(int)_restResponse.StatusCode} ");
            
            //verifying the correct status code is received, when request is successful
           Assert.AreEqual(statusCode, (int)_restResponse.StatusCode, $"Request failed with status code {(int)_restResponse.StatusCode}");
           

        }

        [Then(@"the response should have category name as ""([^""]*)""")]
        public void ThenTheResponseShouldHaveCategoryNameAs(string name)
        {


            if (string.IsNullOrEmpty(_restResponse.Content))
            {
                throw new InvalidOperationException("The response content is null or empty.");
            }
            
            //get Category Details from Json Response :: Call getCategoryDetails method 
            _categoryDetails = _categoryRequests.getCategoryDetails(_restResponse.Content);

            Logger.log.Information($"Category Name from API Response is {_categoryDetails.Name}");
            
            Assert.AreEqual(name, _categoryDetails.Name);
        }

        [Then(@"the response should have CanRelist value  as ""([^""]*)""")]
        public void ThenTheResponseShouldHaveCanRelistValueAs(bool canRelist)
        {
                    

            Logger.log.Information($"Value of CanReList from API Response is {_categoryDetails.CanRelist}");
            Assert.AreEqual(canRelist, _categoryDetails.CanRelist);
        }

        [Then(@"the promotion ""([^""]*)"" should contain text ""([^""]*)""")]
        public void ThenThePromotionShouldContainText(string promotion, string text)
        {

            //Find Description for the given promotion name from Category Details
            if (_categoryDetails.Promotions!=null)
            {
                string promotionDescription = _categoryRequests.getTextForPromotionName(_categoryDetails.Promotions, promotion);
                Logger.log.Information($"The Promotion Description text from API response is {promotionDescription}");
                Assert.AreEqual(text, promotionDescription);
            }
            else
            {
                Logger.log.Error("Promotions List is null ");
            }
           
        }


    }
}
