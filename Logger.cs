using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.IO;

namespace RDR2CS
{
    public static class Logger
    {
        public enum LogLevel
        {
            Verbose = 0,
            Info = 1,
            Warning = 2,
            Error = 3
        }

        // Updated P/Invoke declaration for Logger_Init
        [DllImport("RDONativesd.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool Logger_Init(string projectName, string settingsPath);

        [DllImport("RDONativesd.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Logger_Destroy();

        [DllImport("RDONativesd.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Logger_ToggleConsole(bool toggle);

        [DllImport("RDONativesd.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Logger_Log(int level, string message);

        // Updated Init method to match C++ implementation
        public static bool Init(string projectName, string settingsPath = "/settings.js")
        {
            return Logger_Init(projectName, settingsPath);
        }

        public static void Destroy()
        {
            Logger_Destroy();
        }

        public static void ToggleConsole(bool enable)
        {
            Logger_ToggleConsole(enable);
        }

        // LOG "macro" functionality remains the same
        // LOG "macro" functionality
        public static class LOG
        {
            public static LogLevel MinimumLogLevel { get; set; } = LogLevel.Info;

            public static void VERBOSE(string message,
                [CallerFilePath] string file = "",
                [CallerLineNumber] int line = 0,
                [CallerMemberName] string member = "")
            {
                if (MinimumLogLevel <= LogLevel.Verbose)
                    Logger_Log((int)LogLevel.Verbose, FormatMessage(message, file, line, member));
            }

            public static void INFO(string message,
                [CallerFilePath] string file = "",
                [CallerLineNumber] int line = 0,
                [CallerMemberName] string member = "")
            {
                if (MinimumLogLevel <= LogLevel.Info)
                    Logger_Log((int)LogLevel.Info, FormatMessage(message, file, line, member));
            }

            public static void WARNING(string message,
                [CallerFilePath] string file = "",
                [CallerLineNumber] int line = 0,
                [CallerMemberName] string member = "")
            {
                if (MinimumLogLevel <= LogLevel.Warning)
                    Logger_Log((int)LogLevel.Warning, FormatMessage(message, file, line, member));
            }

            public static void ERROR(string message,
                [CallerFilePath] string file = "",
                [CallerLineNumber] int line = 0,
                [CallerMemberName] string member = "")
            {
                if (MinimumLogLevel <= LogLevel.Error)
                    Logger_Log((int)LogLevel.Error, FormatMessage(message, file, line, member));
            }

            private static string FormatMessage(string message, string file, int line, string member)
            {
                return $"[{Path.GetFileName(file)}:{line} - {member}] {message}";
            }
        }
    }
}