using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDONativesWrapper;

namespace RDR2CS
{
    class Program
    {
        void Main()
        {
            Console.WriteLine("Testing DLL functionality");

            // Test your DLL functions here
            try
            {
                // Example: Initialize the logger
                Logger.Init("RDR2CS", "test.log");
                // Logger.ToggleConsole(true);
                // Log some messages
                LOG.INFO("Testing DLL functionality");
                LOG.INFO("This is an info message");
                LOG.WARNING("This is a warning message");

                // Test other functions from your DLL
                
                Console.WriteLine("DLL functions tested successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
