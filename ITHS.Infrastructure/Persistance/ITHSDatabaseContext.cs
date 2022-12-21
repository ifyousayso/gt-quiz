using ITHS.Domain.Constants;
using ITHS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITHS.Infrastructure.Persistance;

public class ITHSDatabaseContext : DbContext {
    public DbSet<Question> Questions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Language> Languages { get; set; }

    public DbSet<Answer> Answers { get; set; }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Role> Roles { get; set; }

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

//        CreateQuestionsModel(modelBuilder);

//        CreatePersonsModel(modelBuilder);
//        CreateRolesModel(modelBuilder);
    }
        /*
    private static void CreateQuestionsModel(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Question>()
            .HasOne<Category>((question) => question.Category);
        modelBuilder.Entity<Question>()
            .HasOne<Language>((question) => question.Language);
    }
        */
    private static void CreatePersonsModel(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Person>()
            .HasIndex((person) => person.FirstName)
            .IsUnique(false);

        modelBuilder.Entity<Person>()
            .HasOne<Role>((person) => person.Role);
    }

    private static void CreateRolesModel(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Role>()
            .HasIndex((role) => role.RoleName)
            .IsUnique(true);

        SeedDefaultDataToRoleTable(modelBuilder);
    }

    private static void SeedDefaultDataToRoleTable(ModelBuilder modelBuilder) {
        var rolesThatShouldExistInDatabase = new List<Role>() {
            new Role(new Guid("91A90C1B-0639-47F6-81C2-AB38609AEED8"), SchoolRoles.Student),
            new Role(new Guid("5444BE36-4EB9-4823-9336-07028D48E849"), SchoolRoles.Teacher)
        };

        modelBuilder.Entity<Role>().HasData(rolesThatShouldExistInDatabase);
    }
}
