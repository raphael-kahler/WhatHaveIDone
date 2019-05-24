using System.Diagnostics;

namespace Whid.Helpers
{
    internal static class ProcessStarter
    {
        public static Process OpenInBrowser(string url) =>
            Process.Start("cmd", $"/C start {url}");
    }
}
