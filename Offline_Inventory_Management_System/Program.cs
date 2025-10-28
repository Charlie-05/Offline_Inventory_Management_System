using Offline_Inventory_Management_System.DataBase;
using Offline_Inventory_Management_System.Views;

namespace Offline_Inventory_Management_System
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            DbConfig.InitializeDatabase();
            Application.Run(new AuthView());
        }
    }
}