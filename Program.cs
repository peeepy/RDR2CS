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
            string execDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
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
        
        public static int Main()
        {
            try
            {
                Logger.Init("RDR2CS", "test.log");
                Logger.ToggleConsole(true);
                LOG.INFO("C# initialization started");
                LOG.INFO("Initializing native low-level components...");
                
                // Call native low-level initialization
                // string directory = GetFileDirectory("documents");
                FileMgr.Init(GetFileDirectory("documents"));
                LOG.INFO("FileMgr initialized");
                
                Byte_Patch_Manager.Init();
                try
                {
                    // Hooking.Init();
                    // LOG.INFO("Hooking initialized");
                
                    ScriptMgr.Init();
                    LOG.INFO("ScriptMgr initialized");
                
                    FiberPool.Init(5);
                    LOG.INFO("FiberPool initialized");
                }
                catch (Exception ex)
                {
                    LOG.ERROR($"Initialization failed: {ex.Message}");
                    return -1;
                }

                // TODO Make GUI settings class
                LOG.INFO("All native low-level components initialized");
                
                // Other initializations...
                    
                // Start main mod loop or setup
                Notifications.Show("RDR2CS", "Loaded successfully", NotificationType.Success);
                return 0;
            }
            catch (Exception ex)
            {
                LOG.ERROR($"Initialization failed: {ex.Message}");
                return -1;
            }
        }
    }
}
