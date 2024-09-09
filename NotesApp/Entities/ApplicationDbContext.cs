using Microsoft.EntityFrameworkCore;

namespace NotesApp.Entities;

public class ApplicationDbContext : DbContext
{
    public virtual DbSet<Subject> Subjects { get; set; }
    public virtual DbSet<Note> Notes { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subject>().ToTable("Subjects");
        modelBuilder.Entity<Note>().ToTable("Notes");
    }
}