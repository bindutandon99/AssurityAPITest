using AssurityAPITests.Log;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;

namespace AssurityAPITests.Utilities
{

    public class ReportHelper
        {

        protected static ExtentReports _extentReport = new ExtentReports(); // Initialize with default value
        protected static ExtentSparkReporter? _sparkReporter;


        /// <summary>
        /// Initializes the Extent Reports with the necessary configurations for test reporting.
        /// </summary>summary>
        public static void ExtentReportInit()
            {
            
            // Initialize Extent Reports with Spark Reporter
            string reportPath = GetTestReportPath();

            _sparkReporter = new ExtentSparkReporter(reportPath);
            _sparkReporter.Config.ReportName = "Automation Test Report";
            _sparkReporter.Config.DocumentTitle = "Test Results";
            _sparkReporter.Config.Theme = Theme.Standard;

            _extentReport.AttachReporter(_sparkReporter);

            Logger.log.Information($"Test Report initialized is at: {reportPath}");
        }

        /// <summary>
        /// Generate the test report path by navigating to the project root and appending the report location
        /// </summary>
        private static string GetTestReportPath()
        {
            
            var assemblyPath = AppContext.BaseDirectory;
            var actualPath = assemblyPath.Substring(0, assemblyPath.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;
            //Generate Report file name with added timestamp
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var testReportPath = projectPath + $@"Reports\ExtentSparkReport_{AppConfig.environment}_{timestamp}.html";
            return testReportPath;
        }

        /// <summary>
        /// Cleans up the report by flushing any remaining logs and finalizing the report generation
        /// </summary>
        public static void cleanReport()
            {
            
             _extentReport.Flush();
            }

        }
    }


