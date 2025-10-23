using System;
using System.Data.SqlClient;
using TechStore.ConsoleDemo.AppMenu;
using TechStore.DAL;
using TechStore.DAL.Concrete;

namespace TechStore.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TechStore;Integrated Security=True;TrustServerCertificate=True;";
            try
            {
                Console.WriteLine("Attempting to connect to the database...");
                using (var context = new TechStoreDbContext(connectionString))
                {
                    var categoryDal = new CategoryDAL(context);
                   // categoryDal.GetAll();

                    Console.WriteLine("Connection established successfully. Starting menu.");
                    var menu = new Menu(context);
                    menu.Start();
                }
            }
            catch (SqlException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n=======================================================");
                Console.WriteLine("SQL RUNTIME EXCEPTION:");
                Console.WriteLine($"SQL Error Code: {ex.Number}");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("\nVerification required:");
                Console.WriteLine("1. Ensure the database 'TechStore' exists.");
                Console.WriteLine("2. If using a named SQL instance, change 'localhost' (e.g., to 'localhost\\SQLEXPRESS').");
                Console.WriteLine("Press any key to exit.");
                Console.WriteLine("=======================================================");
                Console.ResetColor();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n=======================================================");
                Console.WriteLine("CRITICAL EXCEPTION:");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to exit.");
                Console.WriteLine("=======================================================");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }
}
