using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Ecommerce.Data.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) 
    { 
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Reviews> Reviews { get; set; }
    public DbSet<Notifications> Notifications { get; set; }
    public DbSet<ProductCart> ProductCarts { get; set; }
    public DbSet<Cart> Cart { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 
        
        modelBuilder.Entity<User>().HasMany(u => u.Order)
            .WithOne(o => o.User)
            .HasForeignKey(od=>od.UserId);
        
        modelBuilder.Entity<User>().HasMany(u => u.Notifications)
            .WithOne(n => n.User)
            .HasForeignKey(re => re.UserId);

        modelBuilder.Entity<User>().HasMany(u => u.Reviews)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId);

        modelBuilder.Entity<Order>().HasMany(o => o.OrderDetail)
            .WithOne(d => d.Order)
            .HasForeignKey(d => d.OrderId);

        modelBuilder.Entity<Product>().HasMany(p => p.reviews)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId);
        modelBuilder.Entity<Product>().HasMany(p => p.OrderDetails)
            .WithOne(o => o.Product)
            .HasForeignKey(or => or.ProductId);
        
        
        modelBuilder.Entity<Cart>()
            .HasOne(c => c.User)
            .WithOne(u=>u.Cart)
            .HasForeignKey<Cart>(c => c.UserId)
            .IsRequired(false); // Marca la relación como opcional

        modelBuilder.Entity<Category>().HasMany(c => c.ProductsList)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);
        
        modelBuilder.Entity<ProductCart>()
            .HasKey(pc => new { pc.CartId, pc.ProductId });

        modelBuilder.Entity<ProductCart>()
            .HasOne(cp => cp.Cart)
            .WithMany(c => c.ProductCart)
            .HasForeignKey(cp => cp.CartId);

        modelBuilder.Entity<ProductCart>()
            .HasOne(cp => cp.Product)
            .WithMany(p => p.ProductCart)
            .HasForeignKey(cp => cp.ProductId);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.UserSender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.UserReceptor)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(m => m.ReceptorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        
    
        base.OnModelCreating(modelBuilder);
    }
}