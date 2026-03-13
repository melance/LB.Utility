using System.Diagnostics;

namespace LB.Utility
{
    public sealed class StaticMethods
    {
        private StaticMethods() { }

        public static void OpenInExplorer(String path) => OpenInExplorer(path, true);

        public static void OpenInExplorer(String path, Boolean errorIfNotFound)
        {
            if (Directory.Exists(path))
            {
                var info = new ProcessStartInfo("explorer.exe", path);
                Process.Start(info);
            }
            else if (errorIfNotFound)
                throw new ArgumentException("Path not found.");
        }

        public static void SelectInExplorer(String path) => SelectInExplorer(path, true);

        public static void SelectInExplorer(String path, Boolean errorIfNotFound)
        {
            if (File.Exists(path))
            {
                var info = new ProcessStartInfo("explorer.exe", $"/select,{path}");
                Process.Start(info);
            }
            else if (errorIfNotFound)
                throw new ArgumentException("Path not found.");
        }

        public static void OpenURL(Uri url) => OpenURL(url.ToString());

        public static void OpenURL(String url)
        {
            var info = new ProcessStartInfo(url)
            {
                UseShellExecute = true,
                Verb = "open"
            };

            Process.Start(info);
        }
    }
}
