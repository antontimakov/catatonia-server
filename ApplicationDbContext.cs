//ApplicationDbContext.cs
using CatatoniaServer.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Elem> elem { get; set; }
    public DbSet<Field> field { get; set; }
    public DbSet<FieldElem> field_elem { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка связей между таблицами
        //modelBuilder.Entity<Field_elem>()
        //    .HasKey(fe => new { fe.field_id, fe.elem_id, fe.x, fe.y, fe.field_order});

        modelBuilder.Entity<FieldElem>()
            .HasOne(fe => fe.field)
            .WithMany(f => f.field_elems)
            .HasForeignKey(fe => fe.field_id);

        modelBuilder.Entity<FieldElem>()
            .HasOne(fe => fe.elem)
            .WithMany(e => e.field_elems)
            .HasForeignKey(fe => fe.elem_id);
    }
}