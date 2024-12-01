

/// <summary>
/// A builder class for constructing API endpoint URIs 
/// </summary>summary>
namespace AssurityAPITests.Builders
{

    /// <summary>
    /// Constructs the URI for the categories endpoint 
    /// </summary>
    /// <param name="id">The identifier for the category.</param>
    /// <param name="catalogue">The catalogue name or identifier.</param>
    /// <returns>A formatted string representing the complete category endpoint URI.</returns>
    internal class ApiEndPointBuilder 
    {
        protected String GetCategoriesUri(string id, string catalogue) => String.Format(AppConfig.GetConfigValue("CategoryEndpoint"), id, catalogue);
              
    }
}

