//ApplicationDbContext.cs
using CatatoniaServer.Modules.MainField.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<ElemModel> elem { get; set; }
    public DbSet<FieldModel> field { get; set; }
    public DbSet<FieldElemModel> field_elem { get; set; }
    public DbSet<UserModel> user { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка связей между таблицами
        //modelBuilder.Entity<Field_elem>()
        //    .HasKey(fe => new { fe.field_id, fe.elem_id, fe.x, fe.y, fe.field_order});

        modelBuilder.Entity<FieldElemModel>()
            .HasOne(fe => fe.field)
            .WithMany(f => f.field_elems)
            .HasForeignKey(fe => fe.field_id);

        modelBuilder.Entity<FieldElemModel>()
            .HasOne(fe => fe.elem)
            .WithMany(e => e.field_elems)
            .HasForeignKey(fe => fe.elem_id);
    }
}