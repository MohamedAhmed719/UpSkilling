namespace Task.Api.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{

    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
