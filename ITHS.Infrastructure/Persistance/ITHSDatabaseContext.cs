using ITHS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITHS.Infrastructure.Persistance;

public class ITHSDatabaseContext : DbContext {
    public DbSet<Question> Questions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Language> Languages { get; set; }

    public DbSet<Answer> Answers { get; set; }

    public ITHSDatabaseContext() : base() {}

    public ITHSDatabaseContext(DbContextOptions<ITHSDatabaseContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (!optionsBuilder.IsConfigured) {
            optionsBuilder.UseSqlite("Data Source=sqlite.db");
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
    }
}
