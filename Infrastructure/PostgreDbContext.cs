using Domain.Entities;
using Domain.Entities.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class PostgreDbContext : DbContext
{
    private readonly string _connectionString;
    
    public DbSet<TraderItem> TraderItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Trader> Traders { get; set; }
    
    public PostgreDbContext(IConfiguration configuration)
    {
        var readedConnString = configuration.GetConnectionString("DefaultConnection");

        _connectionString = readedConnString ?? throw new Exception("Connection string \"DefaultConnection\" wasn't found in appsettings.json");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}