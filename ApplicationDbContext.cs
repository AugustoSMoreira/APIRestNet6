using Microsoft.EntityFrameworkCore;
// Configurações com o Banco de Dados
public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>()
            .Property(p => p.Code).HasMaxLength(20).IsRequired();
        builder.Entity<Product>()
            .Property(p => p.Mark).HasMaxLength(50).IsRequired();
        builder.Entity<Product>()
            .Property(p => p.Amount).HasMaxLength(10).IsRequired();
        builder.Entity<Category>().ToTable("Categories");
    }
}