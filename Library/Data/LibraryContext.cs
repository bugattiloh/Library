using System.Reflection;
using Library.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Reader> Readers { get; set; }
    public DbSet<Book> Books { get; set; }
    
}