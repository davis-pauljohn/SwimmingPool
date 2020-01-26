using DbUp;
using System;
using System.Linq;
using System.Reflection;

namespace SwimmingPool.DatabaseMigration
{
    public class MigrationRunner
    {
        private static int Main(string[] args)
        {
            var connectionString = args.FirstOrDefault() ?? "Server=127.0.0.1;Port=5432;Database=swimmingpool;User Id=swimmingpool_admin;Password=Technica7Audio;";

            EnsureDatabase.For.PostgresqlDatabase(connectionString);

            var upgrader = DeployChanges.To.PostgresqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }
    }
}