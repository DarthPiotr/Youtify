using System.Diagnostics;

namespace YoutifyLib
{
    /// <summary>
    /// Utilities class
    /// </summary>
    public static class Utils
    {
        ///////////////////////////////////////
        //    Debug Logs

        public static void LogError(string message)
            => Debug.WriteLine("[!!ERROR]: " + message);
        public static void LogError(string format, params object[] args)
            => Debug.WriteLine("[!!ERROR]: " + string.Format(format, args));

        public static void LogWarning(string message)
            => Debug.WriteLine("[!WARN]: " + message);
        public static void LogWarning(string format, params object[] args)
            => Debug.WriteLine("[!WARN]: " + string.Format(format, args));

        public static void LogInfo(string message)
            => Debug.WriteLine("[INFO]: " + message);
        public static void LogInfo(string format, params object[] args)
            => Debug.WriteLine("[INFO]: " + string.Format(format, args));
    }
}
