using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TruekeCuba.Config.Configs;
using TruekeCuba.Infrastructure.Models;

namespace TruekeCuba.Infrastructure.Data;



public partial class TruekeCubaDb : DbContext
{
    private static readonly string ConnectionString = $"Host={DbConfig.Instance.Host};Database={DbConfig.Instance.Database};Username={DbConfig.Instance.Username};Password={DbConfig.Instance.Password};SslMode=Prefer;";
   
    
    public DbSet<User> Users{ get; set; }
    
    public TruekeCubaDb(DbContextOptions<TruekeCubaDb> options) : base(options)
    {
        
    }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.ConfigureWarnings(w => w.Ignore(RelationalEventId.ForeignKeyPropertiesMappedToUnrelatedTables));
        options.UseNpgsql(ConnectionString);
    }
    
    
    

}