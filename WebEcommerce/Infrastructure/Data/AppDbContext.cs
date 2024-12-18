using Microsoft.EntityFrameworkCore;
using WebEcommerce.Domain.Entities;

namespace WebEcommerce.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Category> Categories { get; set; }

    
}