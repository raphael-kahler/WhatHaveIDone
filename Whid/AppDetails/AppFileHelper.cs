using System;
using System.IO;

namespace Whid.AppDetails
{
    internal class AppFileHelper
    {
        /// <summary>
        /// Create the application directory if it doesn't exist yet.
        /// </summary>
        public void EnsureApplicationDirectoryExists() =>
            Directory.CreateDirectory(GetApplicationDirectory());

        /// <summary>
        /// Get the path of the application directory.
        /// </summary>
        /// <returns>The application directory path.</returns>
        public string GetApplicationDirectory() =>
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Constants.ApplicationName);

        /// <summary>
        /// Get the path to a file in the application directory.
        /// </summary>
        /// <param name="fileName">The requested file.</param>
        /// <returns>The file path of the file in the application directory.</returns>
        public string GetApplicationFilePath(string fileName) =>
            Path.Combine(GetApplicationDirectory(), fileName);
    }
}
