using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace FlutterBackEnd.Data
{
    public class MyContext : DbContext
    {
        private String connectionString = "Server=tcp:sql-flutter-server.database.windows.net,1433;Initial Catalog=sql-flutter-db;Persist Security Info=False;User ID=BigBoss;Password=Password123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Models.GroceryModel> GroceryItem { get; set; }
    }
}
