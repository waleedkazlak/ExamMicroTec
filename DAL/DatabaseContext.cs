using ExamMicroTec.DAL.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace ExamMicroTec.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext>  options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnectionString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
