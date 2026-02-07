//ApplicationDbContext.cs
using CatatoniaServer.Modules.MainField.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<ElemModel> Elem { get; set; }
    public DbSet<FieldModel> Field { get; set; }
    public DbSet<FieldElemModel> FieldElem { get; set; }
    public DbSet<UserModel> User { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Таблицы в snake_case
            entity.SetTableName(ToSnakeCase(entity.GetTableName()));
            
            // Колонки в snake_case
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.Name));
            }
            
            // Ключи в snake_case
            foreach (var key in entity.GetKeys())
            {
                key.SetName(ToSnakeCase(key.GetName()));
            }
            
            // Индексы в snake_case
            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()));
            }
        }

        modelBuilder.Entity<FieldElemModel>()
            .HasOne(fe => fe.Field)
            .WithMany(f => f.FieldElems)
            .HasForeignKey(fe => fe.FieldId);

        modelBuilder.Entity<FieldElemModel>()
            .HasOne(fe => fe.Elem)
            .WithMany(e => e.FieldElems)
            .HasForeignKey(fe => fe.ElemId);
    }

    private string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        
        return string.Concat(input.Select((x, i) => 
            i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()))
            .ToLower();
    }
}