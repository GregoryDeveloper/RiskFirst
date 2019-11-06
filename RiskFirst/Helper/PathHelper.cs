using System;
using System.Collections.Generic;
using System.Text;

namespace RiskFirst.Helper
{
    public static class PathHelper
    {
        public static string ProjectDirectoryPath { get { return GetProjectDirectoryPath(); } }

        private static string GetProjectDirectoryPath()
        {
            string basePath = Environment.CurrentDirectory;
            int index = basePath.IndexOf("\\RiskFirst");
            return Environment.CurrentDirectory.Substring(0, index);
        }
    }
}
