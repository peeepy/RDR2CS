using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Runtime.InteropServices;
using RDR2CS;

namespace RDR2CS
{
    public class Program
    {
        private static string GetFileDirectory(string subdir)
        {
            string execDir = Environment.GetEnvironmentVariable("appdata");
            if (execDir == null)
            {
                throw new InvalidOperationException("Unable to determine the executable directory.");
            }

            string subDir = subdir; // Replace with your subdirectory name

            // Combine the executable directory with the subdirectory name
            string fullPath = Path.Combine(execDir, subDir);

            // Ensure the directory exists
            string documents = Directory.Exists(fullPath) ? fullPath : Directory.CreateDirectory(fullPath).FullName;
            return documents;
        }

        private static bool _isInitialized = false;
        private static readonly object _lockObject = new object();

        public static void Main()
        {
            lock (_lockObject)
            {
                if (!_isInitialized)
                {
                    try
                    {
                        EmergencyLogger.LogError("Initialization call stack: " + Environment.StackTrace);
                        EmergencyLogger.LogError(
                            $"Initialization started on thread {Thread.CurrentThread.ManagedThreadId}");
                        EmergencyLogger.LogError("Starting initialization...");
                        try
                        {
                            FileMgr.Init(GetFileDirectory("RDR2CS"));
                            Logger.Init("RDR2CS", FileMgr.GetProjectFile("./output.log"));
                            EmergencyLogger.LogError("Logger initialized successfully.");
                        }
                        catch (Exception ex)
                        {
                            EmergencyLogger.LogError($"Logger initialization failed: {ex.Message}");
                            EmergencyLogger.LogError($"Stack Trace: {ex.StackTrace}");
                        }

                        try
                        {
                            LOG.INFO("C# initialization started");
                            EmergencyLogger.LogError("First LOG.INFO call successful");
                        }
                        catch (Exception ex)
                        {
                            EmergencyLogger.LogError($"First LOG.INFO call failed: {ex.Message}");
                            EmergencyLogger.LogError($"Stack Trace: {ex.StackTrace}");
                        }

                        try
                        {
                            LOG.INFO("Initializing native low-level components...");
                            EmergencyLogger.LogError("Second LOG.INFO call successful");
                        }
                        catch (Exception ex)
                        {
                            EmergencyLogger.LogError($"Second LOG.INFO call failed: {ex.Message}");
                            EmergencyLogger.LogError($"Stack Trace: {ex.StackTrace}");
                        }

                        // Call native low-level initialization
                        try
                        {

                            Byte_Patch_Manager.Init();
                            LOG.INFO("Byte_Patch_Manager initialized");
                            EmergencyLogger.LogError("Byte_Patch_Manager initialized successfully.");
                        }
                        catch (Exception ex)
                        {
                            EmergencyLogger.LogError($"Byte_Patch_Manager initialization failed: {ex.Message}");
                            EmergencyLogger.LogError($"Stack Trace: {ex.StackTrace}");
                        }

                        // try
                        // {
                        //     Hooking.Init();
                        //     LOG.INFO("Hooking initialized");
                        //     EmergencyLogger.LogError("Hooking initialized successfully.");
                        // }
                        // catch (Exception ex)
                        // {
                        //     EmergencyLogger.LogError($"Hooking initialization failed: {ex.Message}");
                        //     EmergencyLogger.LogError($"Stack Trace: {ex.StackTrace}");
                        // }

                        // NOTE: ScriptMgr STOPS THE FUNCTION FROM RUNNING! TODO: FIX.
                        // try
                        // {
                        //     ScriptMgr.Init();
                        //     LOG.INFO("ScriptMgr initialized");
                        //     EmergencyLogger.LogError("ScriptMgr initialized successfully.");
                        // }
                        // catch (Exception ex)
                        // {
                        //     EmergencyLogger.LogError($"ScriptMgr initialization failed: {ex.Message}");
                        //     EmergencyLogger.LogError($"Stack Trace: {ex.StackTrace}");
                        // }

                        try
                        {
                            FiberPool.Init(5);
                            LOG.INFO("FiberPool initialized");
                            EmergencyLogger.LogError("FiberPool initialized successfully.");
                        }
                        catch (Exception ex)
                        {
                            EmergencyLogger.LogError($"FiberPool initialization failed: {ex.Message}");
                            EmergencyLogger.LogError($"Stack Trace: {ex.StackTrace}");
                        }
                    }
                    catch (Exception ex)
                    {
                        LOG.WARNING($"Exception in Main: {ex.Message}");
                        EmergencyLogger.LogError($"Exception in Main: {ex.Message}");
                        EmergencyLogger.LogError($"Stack Trace: {ex.StackTrace}");
                    }

                    LOG.INFO("All native low-level components initialized");

                    // Other initializations...

                    // Start main mod loop or setup
                    Notifications.Show("RDR2CS", "Loaded successfully", NotificationType.Success);
                    // Your initialization code here
                    _isInitialized = true;
                }
                else
                {
                    {
                        EmergencyLogger.LogError("Initialization already completed. Skipping.");
                    }
                }

            }
        }
    }
}
