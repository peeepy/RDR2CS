// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using RDR2CS.oldcode;
//
// namespace RDR2CS
// {
//     public class GUI
//     {
//         private bool m_IsOpen;
//         private static GUI instance;
//
//         // private constructor prevents direct instantiation from outside
//         private GUI()
//         {
//             m_IsOpen = false;
//             Menu.SetupStyle();
//             // Menu.Init();
//
//         }
//
//         // Singleton pattern to get the single instance of the GUI class
//         public static GUI GetInstance()
//         {
//             // if (instance == null)
//             // {
//             //     instance = new GUI();
//             // }
//             return instance ??= new GUI();
//         }
//
//         // Static method to check if the GUI is open
//         public static bool IsOpen()
//         {
//             return GetInstance().m_IsOpen;
//         }
//
//         // Static method to toggle the GUI's open state
//         public static void Toggle()
//         {
//             GUI gui = GetInstance();
//             gui.m_IsOpen = !gui.m_IsOpen;
//         }
//     }
// }
