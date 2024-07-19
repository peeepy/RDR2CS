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

        [DllImport("RDONatives.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool Logger_Init(string projectName, string settingsPath, bool attachConsole);

        [DllImport("RDONatives.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Logger_Destroy();

        [DllImport("RDONatives.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Logger_ToggleConsole(bool toggle);

        [DllImport("RDONatives.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Logger_Log(int level, string message);

        public static bool Init(string projectName, string settingsPath = "/settings.js", bool attachConsole = true)
        {
            EmergencyLogger.LogError($"Attempting to initialize logger. Project: {projectName}, Settings: {settingsPath}, AttachConsole: {attachConsole}");
            try
            {
                bool result = Logger_Init(projectName, settingsPath, attachConsole);
                if (!result)
                {
                    int error = Marshal.GetLastWin32Error();
                    EmergencyLogger.LogError($"Logger_Init failed. Error code: {error}");
                }
                else
                {
                    EmergencyLogger.LogError("Logger_Init succeeded");
                }
                return result;
            }
            catch (Exception ex)
            {
                EmergencyLogger.LogError($"Exception in Logger.Init: {ex.Message}");
                EmergencyLogger.LogError($"Stack Trace: {ex.StackTrace}");
                return false;
            }
        }

        public static void Destroy()
        {
            try
            {
                Logger_Destroy();
                EmergencyLogger.LogError("Logger_Destroy called successfully");
            }
            catch (Exception ex)
            {
                EmergencyLogger.LogError($"Exception in Logger.Destroy: {ex.Message}");
                EmergencyLogger.LogError($"Stack Trace: {ex.StackTrace}");
            }
        }

        public static void ToggleConsole(bool enable)
        {
            try
            {
                Logger_ToggleConsole(enable);
                EmergencyLogger.LogError($"Logger_ToggleConsole called with parameter: {enable}");
            }
            catch (Exception ex)
            {
                EmergencyLogger.LogError($"Exception in Logger.ToggleConsole: {ex.Message}");
                EmergencyLogger.LogError($"Stack Trace: {ex.StackTrace}");
            }
        }

        public static class LOG
        {
            public static LogLevel MinimumLogLevel { get; set; } = LogLevel.Info;

            public static void VERBOSE(string message,
                [CallerFilePath] string file = "",
                [CallerLineNumber] int line = 0,
                [CallerMemberName] string member = "")
            {
                LogWithLevel(LogLevel.Verbose, message, file, line, member);
            }

            public static void INFO(string message,
                [CallerFilePath] string file = "",
                [CallerLineNumber] int line = 0,
                [CallerMemberName] string member = "")
            {
                LogWithLevel(LogLevel.Info, message, file, line, member);
            }

            public static void WARNING(string message,
                [CallerFilePath] string file = "",
                [CallerLineNumber] int line = 0,
                [CallerMemberName] string member = "")
            {
                LogWithLevel(LogLevel.Warning, message, file, line, member);
            }

            public static void ERROR(string message,
                [CallerFilePath] string file = "",
                [CallerLineNumber] int line = 0,
                [CallerMemberName] string member = "")
            {
                LogWithLevel(LogLevel.Error, message, file, line, member);
            }

            private static void LogWithLevel(LogLevel level, string message, string file, int line, string member)
            {
                if (level >= MinimumLogLevel)
                {
                    try
                    {
                        string formattedMessage = FormatMessage(message, file, line, member);
                        Logger_Log((int)level, formattedMessage);
                    }
                    catch (Exception ex)
                    {
                        EmergencyLogger.LogError($"Exception in Logger.LOG.LogWithLevel: {ex.Message}");
                        EmergencyLogger.LogError($"Stack Trace: {ex.StackTrace}");
                    }
                }
            }

            private static string FormatMessage(string message, string file, int line, string member)
            {
                return $"[{Path.GetFileName(file)}:{line} - {member}] {message}";
            }
        }
    }
}