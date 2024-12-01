using AssurityAPITests.Log;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin;
using AventStack.ExtentReports.Gherkin.Model;

namespace AssurityAPITests.Hooks
{


    /// <summary>
    /// This class is used to set up and tear down test execution,log relevant information, and initialize the reporting process. 
    /// It extends the `ReportHelper` class to handle reporting functionalities.
    /// </summary>

    [Binding]
    public class Hooks : Utilities.ReportHelper
    {
        private static ExtentTest? _feature;   // Nullable type to handle potential null reference
        private static ExtentTest? _scenario;


        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Logger.log.Information("BeforeTestRun: initializing Resources");
            
            //calling method to initialize test  Extent report
             ExtentReportInit();
  
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Logger.log.Information("AfterTestRun: Cleaning up resources...");
            cleanReport();
            Logger.log.Information("Test Report finalized.");
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Logger.log.Information($"BeforeFeature: Starting feature - {featureContext.FeatureInfo.Title}");

            // Create a feature entry in the Extent Report
            _feature = _extentReport.CreateTest<Feature>(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Description);
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            Logger.log.Information("AfterFeature: Completed feature execution.");
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            string title = scenarioContext.ScenarioInfo.Title;
            string description = scenarioContext.ScenarioInfo.Description ?? "No description provided.";

            Logger.log.Information($"Starting scenario: {title}");

            if (_feature == null)
            {
                Logger.log.Error("Feature not initialized before scenario.");
            }
            else
            {
                _scenario = _feature.CreateNode<Scenario>(title, description);
            }
        }

        [AfterStep]
        public void AfterEachStep(ScenarioContext scenarioContext)
        {
            try
            {
                var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
                var stepName = scenarioContext.StepContext.StepInfo.Text;

                if (scenarioContext.TestError != null)
                {
                    _scenario?.CreateNode(new GherkinKeyword(stepType), stepName)
                             .Fail($"");
                    Logger.log.Error($"Step - {stepType} {stepName}  failed");
                }
                else
                {
                    _scenario?.CreateNode(new GherkinKeyword(stepType), stepName).Pass($"");
                    Logger.log.Information($"Step - {stepType} {stepName}  passed");
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error($"Error in AfterEachStep: {ex.Message}\n{ex.StackTrace}");
            }
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            

            if (scenarioContext.TestError != null)
            {
                Logger.log.Error($"Scenario {scenarioContext.ScenarioInfo.Title} Failed ");
                _scenario?.Fail($"Scenario failed: {scenarioContext.TestError.Message}");
            }
            else
            {
                Logger.log.Information("Passed Scenario");
                _scenario?.Pass($"Scenario {scenarioContext.ScenarioInfo.Title} passed.");
            }
            Logger.log.Information($"AfterScenario: Finished scenario - {scenarioContext.ScenarioInfo.Title}");
            
        }

        

       
    }
}
