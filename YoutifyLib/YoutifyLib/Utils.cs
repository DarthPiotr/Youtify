using System.Diagnostics;

namespace YoutifyLib
{
    /// <summary>
    /// Utilities class
    /// </summary>
    public static class Utils
    {
        ///////////////////////////////////////
        //
        //    Debug Logs

        public static void LogError(string message)
            => Debug.WriteLine("[!!ERROR]: " + message);

        public static void LogWarning(string message)
            => Debug.WriteLine("[!WARN]: " + message);

        public static void LogInfo(string message)
            => Debug.WriteLine("[INFO]: " + message);
    }
}
