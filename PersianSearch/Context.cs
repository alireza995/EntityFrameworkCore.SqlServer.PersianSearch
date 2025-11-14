using Microsoft.EntityFrameworkCore;

namespace PersianSearch;

public class Context : DbContext
{
    public Context() { }

    public Context(DbContextOptions<Context> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=MyTestDb;Trusted_Connection=true;TrustServerCertificate=true;"
            );
        }
    }
    
    public DbSet<TblTest> TblTests { get; set; }
}