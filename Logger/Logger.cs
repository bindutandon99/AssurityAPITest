using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssurityAPITests.Log
{

    /// <summary>
    /// A static class that provides a logger instance for the application.
    ///</summary>
    internal static class Logger
    {
        
        /// <summary>
        /// Gets the logger instance for logging messages to a file.
        /// </summary>
        public static ILogger log { get; }

        
        static Logger()
        {
           // Configures the logger to write logs to a file with the path determined by the `GetLogFilePath()` method.
           log = new LoggerConfiguration().WriteTo.File(GetLogFilePath())
                                         .CreateLogger();            
        }

        /// <summary>
        /// Returns the path for the log file where the logs will be stored.
        /// </summary>
        /// <returns>The file path for the log file.</returns>
        private static string GetLogFilePath()
        {
            
            var assemblyPath = AppContext.BaseDirectory;
            var actualPath = assemblyPath.Substring(0, assemblyPath.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;
            string logDirectory = Path.Combine(projectPath, "Logs");
            // Generate a unique log file name with a timestamp
            string logFileName = $"{AppConfig.environment}_log_{DateTime.Now:yyyyMMdd_HHmmss}.log";
            // Full log file path
            string logFilePath = Path.Combine(logDirectory, logFileName);
            
            return logFilePath;
        }
    }
}

